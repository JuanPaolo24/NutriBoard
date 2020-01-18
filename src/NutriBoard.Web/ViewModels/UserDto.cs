using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutriBoard.Web.ViewModels
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string EmailConfirmed { get; set; }
        public Guid ActivationToken { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
