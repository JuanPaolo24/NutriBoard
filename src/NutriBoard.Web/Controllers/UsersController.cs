using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NutriBoard.Core.Entities;
using NutriBoard.Infrastructure.Helpers;
using NutriBoard.Infrastructure.Repositories;
using NutriBoard.Web.Helpers;
using NutriBoard.Web.Services;
using NutriBoard.Web.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NutriBoard.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly IEmailService _emailService;
        private IMapper _mapper;
        private readonly IOptions<AppSettings> _appSettings;

        public UsersController(IUserService userService, IEmailService emailService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _emailService = emailService;
            _mapper = mapper;
            _appSettings = appSettings;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.UserData, user.Username)

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            if (user.EmailConfirmed == false)
                return Accepted(new
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Token = tokenString
                });

            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = tokenString
            });
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            try
            {
                user.ActivationToken = Guid.NewGuid();
                _userService.Create(user, userDto.Password);
                var link = "http://localhost:6060/confirmation.html?id=" + user.Id + "&userToken=" + user.ActivationToken;
                var message = "Hi, " + user.Username + " please click the link attached to this email to activate your account " + link;
                Task.WaitAll(_emailService.SendEmailAsync(user.Email, "Confirmation Link", message));

                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }


        }

        [AllowAnonymous]
        [HttpPost("password")]
        public IActionResult SendPasswordReset([FromBody] string email)
        {
            //This will be the end point for handling sending the email to the user

            //Check if email exist currently and if it is tied to a user
            var user = _userService.GetByEmail(email);

            if (user == null)
                return BadRequest(new { message = "That email does not exist!" });

            var link = "http://localhost:6060/changepassword.html?token=" + user.ActivationToken;
            var message = "Hi, please click the link attached to this email to reset your password " + link;
            Task.WaitAll(_emailService.SendEmailAsync(email, "Reset your password", message));

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("resetpassword")]
        public IActionResult ResetPassword([FromQuery] Guid token, [FromBody] string newPassword)
        {
            var user = _userService.GetByToken(token);

            _userService.Update(user, newPassword);
            return Ok();

        }

        [AllowAnonymous]
        [HttpPost("confirm")]
        public IActionResult EmailLink([FromQuery] int id, [FromQuery] Guid userToken)
        {
            _userService.VerifyUser(id, userToken);

            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                _userService.Update(user, userDto.Password);
                return Ok();
            }
            catch (AppException ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

    }
}
