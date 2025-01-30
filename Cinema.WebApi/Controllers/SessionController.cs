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
        /// Отримати сеанс за айді
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
        /// Додати новий сеанс
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(IBaseResponse<string>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(IBaseResponse<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddSession([FromBody] AddSessionDTO sessionDto)
        {
            var response = await _sessionService.AddSessionAsync(sessionDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Редагувати сеанс
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IBaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IBaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateSession(long id, [FromBody] UpdateSessionDTO sessionDto)
        {
            var response = await _sessionService.UpdateSessionAsync(id, sessionDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Видалити сеанс
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IBaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IBaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteSession(long id)
        {
            var response = await _sessionService.DeleteSessionAsync(id);
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
        /// Отримати сеанс по айді фільму
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
