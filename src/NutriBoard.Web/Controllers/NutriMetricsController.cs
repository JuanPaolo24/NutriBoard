using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NutriBoard.Infrastructure.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using NutriBoard.Web.ViewModels;
using NutriBoard.Core.Entities;

namespace NutriBoard.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NutriMetricsController : ControllerBase
    {
        private INutriMetricsRepository _nutriMetricsRepository;
        private IMapper _mapper;

        public NutriMetricsController(INutriMetricsRepository nutriMetricsRepository, IMapper mapper)
        {
            _nutriMetricsRepository = nutriMetricsRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllNutriMetrics()
        {
            var results = await _nutriMetricsRepository.GetAllMetricsAsync();

            return Ok(results);
        }

        [HttpGet("date")]
        public async Task<ActionResult> GetNutriMetricsByDate([FromQuery] string date)
        {
            if (date == null)
                return BadRequest(new { message = "The date you have entered is null" });

            var results = await _nutriMetricsRepository.GetNutriMetricsByDateAsync(date);

            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<NutriMetricsViewModel>> AddNutriMetrics([FromBody] NutriMetricsViewModel nutriMetricsViewModel)
        {

            if (nutriMetricsViewModel == null) return NoContent();

            var nutriMetrics = _mapper.Map<NutriMetrics>(nutriMetricsViewModel);

            _nutriMetricsRepository.Add(nutriMetrics);
            await _nutriMetricsRepository.SaveChangesAsync();
            return Ok(_mapper.Map<NutriMetricsViewModel>(nutriMetrics));

        }

        [HttpDelete]
        public async Task<ActionResult<NutriMetricsViewModel>> DeleteNutriMetrics([FromQuery] int nutriMetricId)
        {
            var nutriMetrics = await _nutriMetricsRepository.GetNutriMetricsById(nutriMetricId);

            if (nutriMetrics == null) return NotFound($"Could not find a nutri metric with the id of {nutriMetricId}");

            _nutriMetricsRepository.Delete(nutriMetrics);
            await _nutriMetricsRepository.SaveChangesAsync();

            return Accepted("Nutrimetric with ID " + nutriMetrics.Id + " deleted");
        }

    }
}
