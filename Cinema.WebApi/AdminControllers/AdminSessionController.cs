using Cinema.Application.DTO.SessionDTOs;
using Cinema.Application.Helpers.Interfaces;
using Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cinema.WebApi.AdminControllers
{
    [Route("api/admin/Sessions")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminSessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public AdminSessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        /// <summary>
        /// Додати новий сеанс (тільки для адміністратора)
        /// </summary>
        [HttpPost("Add")]
        [ProducesResponseType(typeof(IBaseResponse<string>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(IBaseResponse<string>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Add([FromBody] AddSessionDTO sessionDto)
        {
            var response = await _sessionService.AddSessionAsync(sessionDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Редагувати сеанс (тільки для адміністратора)
        /// </summary>
        [HttpPut]
        [Route("[action]/{id}")]
        [ProducesResponseType(typeof(IBaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IBaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateSessionDTO sessionDto)
        {
            var response = await _sessionService.UpdateSessionAsync(id, sessionDto);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Видалити сеанс (тільки для адміністратора)
        /// </summary>
        [HttpDelete]
        [Route("[action]/{id}")]
        [ProducesResponseType(typeof(IBaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IBaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _sessionService.DeleteSessionAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
