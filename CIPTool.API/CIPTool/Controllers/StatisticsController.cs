using BusinessLogicLayer.Statistics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CIPTool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet("ideas")]
        public async Task<IActionResult> GetIdeasStatistics()
        {
            var statistics =  await statisticsService.GetIdeaStatisticsDto();

            if (statistics == null)
            {
                return BadRequest("No statistics could be generated.");
            }

            return Ok(statistics);
        }
    }
}
