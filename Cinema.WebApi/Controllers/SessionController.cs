using Cinema.Application.DTO.SessionDTOs;
using Cinema.Application.Helpers.Interfaces;
using Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        /// <summary>
        /// Отримати всі сеанси
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IBaseResponse<List<GetSessionDTO>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllSessions()
        {
            var response = await _sessionService.GetAllSessionsAsync();
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Отримати сеанс за ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IBaseResponse<GetSessionDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IBaseResponse<GetSessionDTO>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSessionById(long id)
        {
            var response = await _sessionService.GetSessionByIdAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Отримати сеанс за датою
        /// </summary>
        [HttpGet("by-date/{date}")]
        [ProducesResponseType(typeof(IBaseResponse<List<GetSessionDTO>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSessionsByDate(DateTime date)
        {
            var response = await _sessionService.GetSessionsByDateAsync(date);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Отримати сеанс по ID фільму
        /// </summary>
        [HttpGet("by-movie/{movieId}")]
        [ProducesResponseType(typeof(IBaseResponse<List<GetSessionDTO>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSessionsByMovieId(long movieId)
        {
            var response = await _sessionService.GetSessionsByMovieIdAsync(movieId);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
