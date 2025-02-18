using Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.AdminControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("total-revenue")]
        public async Task<IActionResult> GetTotalRevenue([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var response = await _analyticsService.GetTotalRevenueAsync(start, end);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("top-movies")]
        public async Task<IActionResult> GetTopMovies([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var response = await _analyticsService.GetTopMoviesAsync(start, end);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("revenue-by-period")]
        public async Task<IActionResult> GetRevenueByPeriod([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var response = await _analyticsService.GetRevenueByPeriodAsync(start, end);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}