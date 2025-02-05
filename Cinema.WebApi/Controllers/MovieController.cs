using Cinema.Application.DTO.MovieDTOs;
using Cinema.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Отримати всі фільми
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponse<List<GetMovieDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<List<GetMovieDTO>>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _movieService.GetAllMoviesAsync();

            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Get all formatted movies for home page
        /// </summary>
        [HttpGet("formatted")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BaseResponse<List<GetMovieDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<List<GetMovieDTO>>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetFormattedMovies()
        {
            var response = await _movieService.GetFormattedMovies();
            
            if ((int)response.StatusCode == (int)HttpStatusCode.OK)
            {
                return Ok(response.Data);
            }
            return StatusCode((int)response.StatusCode, response);
        }

        /// <summary>
        /// Отримати фільм за ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseResponse<GetMovieDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<GetMovieDTO>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _movieService.GetMovieByIdAsync(id);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}