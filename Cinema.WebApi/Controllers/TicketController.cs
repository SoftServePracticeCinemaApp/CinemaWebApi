using Cinema.Application.DTO.TicketDTOs;
using Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Отримати всі квитки
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<List<GetTicketDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<List<GetTicketDTO>>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _ticketService.GetAllTicketsAsync();

            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Отримати квиток за ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResponse<GetTicketDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<GetTicketDTO>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            var response = await _ticketService.GetTicketByIdAsync(id);

            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Отримати квитки за ID фільму
        /// </summary>
        [HttpGet("movie/{movieId}")]
        [ProducesResponseType(typeof(BaseResponse<List<GetTicketDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<List<GetTicketDTO>>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByMovieId([FromRoute] int movieId)
        {
            var response = await _ticketService.GetTicketsByMovieIdAsync(movieId);

            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Отримати квитки за ID користувача
        /// </summary>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<List<GetTicketDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<List<GetTicketDTO>>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetByUserId([FromRoute] string userId)
        {
            var response = await _ticketService.GetTicketsByUserIdAsync(userId);

            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Отримати квитки за ID сеансу
        /// </summary>
        [HttpGet("session/{sessionId}")]
        [ProducesResponseType(typeof(BaseResponse<List<GetTicketDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<List<GetTicketDTO>>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetBySessionId([FromRoute] long sessionId)
        {
            var response = await _ticketService.GetTicketsBySessionIdAsync(sessionId);

            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Отримати квитки для певного залу
        /// </summary>
        [HttpGet("hall/{hallId}")]
        [ProducesResponseType(typeof(BaseResponse<List<GetTicketDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<List<GetTicketDTO>>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetForHall([FromRoute] int hallId)
        {
            var response = await _ticketService.GetTicketsForHallAsync(hallId);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}