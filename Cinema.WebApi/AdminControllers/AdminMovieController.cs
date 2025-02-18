using Cinema.Application.DTO.MovieDTOs;
using Cinema.Application.Interfaces;

using Cinema.Infrastructure.Utils;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cinema.WebApi.AdminControllers
{
    [Route("api/admin/Movies")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = StaticDetails.ROLEAdmin)]
	[ApiController]
    //[Authorize(Roles = "Admin")]
    public class AdminMovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public AdminMovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Оновити фільм за ID (тільки для адміністратора)
        /// </summary>
        [HttpPut]
        [Route("[action]/{id}")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMovieDTO updateMovieDto)
        {
            var response = await _movieService.UpdateMovieAsync(id, updateMovieDto);

            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Видалити фільм за ID (тільки для адміністратора)
        /// </summary>
        [HttpDelete]
        [Route("[action]/{id}")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _movieService.DeleteMovieAsync(id);

            return StatusCode((int)response.StatusCode, response);
        }



        /// <summary>
        /// Додати фільм з TMDB за SearchId.
        /// </summary>
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<string>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Add([FromBody]int searchId)
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
      
            var response = await _movieService.AddMovieFromTmdbAsync(searchId);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
