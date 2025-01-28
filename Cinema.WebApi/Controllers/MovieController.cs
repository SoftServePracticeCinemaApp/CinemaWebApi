﻿using Cinema.Application.DTO.MovieDTOs;
using Cinema.Application.Interfaces;
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
        /// Додати новий фільм
        /// </summary>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] AddMovieDTO createMovieDto)
        {
            var response = await _movieService.AddMovieAsync(createMovieDto);

            return response.StatusCode switch
            {
                Application.Enums.StatusCode.Ok => StatusCode((int)HttpStatusCode.Created, response),
                Application.Enums.StatusCode.BadRequest => BadRequest(response),
                _ => BadRequest(response)
            };
        }

        /// <summary>
        /// Отримати всі фільми
        /// </summary>
        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(BaseResponse<List<GetMovieDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<List<GetMovieDTO>>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var response = await _movieService.GetAllMoviesAsync();

            return response.StatusCode switch
            {
                Application.Enums.StatusCode.Ok => Ok(response),
                Application.Enums.StatusCode.NotFound => NotFound(response),
                _ => BadRequest(response)
            };
        }

        /// <summary>
        /// Оновити фільм за ID
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMovieDTO updateMovieDto)
        {
            var response = await _movieService.UpdateMovieAsync(id, updateMovieDto);

            return response.StatusCode switch
            {
                Application.Enums.StatusCode.Ok => Ok(response),
                Application.Enums.StatusCode.NotFound => NotFound(response),
                Application.Enums.StatusCode.BadRequest => BadRequest(response),
                _ => BadRequest(response)
            };
        }

        /// <summary>
        /// Видалити фільм за ID
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse<bool>), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _movieService.DeleteMovieAsync(id);

            return response.StatusCode switch
            {
                Application.Enums.StatusCode.Ok => Ok(response),
                Application.Enums.StatusCode.NotFound => NotFound(response),
                _ => BadRequest(response)
            };
        }
    }
}