using Cinema.Application.DTO.HallDTOs;
using Cinema.Application.Helpers.Interfaces;
using Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly IHallService _hallService;

        public HallController(IHallService hallService)
        {
            _hallService = hallService;
        }

        /// <summary>
        /// ќтримати хол за ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IBaseResponse<GetHallDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IBaseResponse<GetHallDTO>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetHallById(int id)
        {
            var response = await _hallService.GetHallByIdAsync(id);
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// ќновити м≥сц€ у хол≥
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(IBaseResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IBaseResponse<string>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateHallSeats(int id, UpdateHallDTO hallDto)
        {
            var response = await _hallService.UpdateHallSeatsAsync(id, hallDto);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
