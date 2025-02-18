using Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cinema.WebApi.Controllers
{
    [Route("api/movie/{movieId}/rate")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        public async Task<IActionResult> AddRating(int movieId, [FromBody] RatingRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var response = await _ratingService.AddRatingAsync(movieId, userId, request.Rating);
            return StatusCode((int)response.StatusCode, response.Description);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRating(int movieId, [FromBody] RatingRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var response = await _ratingService.AddRatingAsync(movieId, userId, request.Rating);
            return StatusCode((int)response.StatusCode, response.Description);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRating(int movieId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var response = await _ratingService.GetUserRatingAsync(movieId, userId);
            return Ok(response.Data);
        }

        [HttpGet("average")]
        public async Task<IActionResult> GetAverageRating(int movieId)
        {
            var response = await _ratingService.GetAverageRatingAsync(movieId);
            return Ok(response.Data);
        }

        [HttpGet("has-rating")]
        public async Task<IActionResult> HasUserRated(int movieId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            var response = await _ratingService.HasUserRatedAsync(movieId, userId);
            return Ok(response.Data);
        }
    }

    public class RatingRequest
    {
        public int Rating { get; set; }
    }
}
