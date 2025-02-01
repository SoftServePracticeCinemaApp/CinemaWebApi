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
        /// Додати новий квиток
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] AddTicketDTO createTicketDto)
        {
            var response = await _ticketService.AddTicketAsync(createTicketDto);

            return StatusCode((int)response.StatusCode, response);
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
        /// Оновити квиток за ID
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] UpdateTicketDTO updateTicketDto)
        {
            var response = await _ticketService.UpdateTicketAsync(id, updateTicketDto);

            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Видалити квиток за ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var response = await _ticketService.DeleteTicketAsync(id);

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