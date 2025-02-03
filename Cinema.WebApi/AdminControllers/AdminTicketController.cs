using Cinema.Application.DTO.TicketDTOs;
using Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cinema.WebApi.AdminControllers
{
    [Route("api/admin/Tickets")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminTicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public AdminTicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        /// <summary>
        /// Додати новий квиток (тільки для адміністратора)
        /// </summary>
        [HttpPost("Add")]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] AddTicketDTO createTicketDto)
        {
            var response = await _ticketService.AddTicketAsync(createTicketDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Оновити квиток за ID (тільки для адміністратора)
        /// </summary>
        [HttpPut]
        [Route("[action]/{id}")]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTicketDTO updateTicketDto)
        {
            var response = await _ticketService.UpdateTicketAsync(id, updateTicketDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Видалити квиток за ID (тільки для адміністратора)
        /// </summary>
        [HttpDelete]
        [Route("[action]/{id}")]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _ticketService.DeleteTicketAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
