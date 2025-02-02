using AutoMapper;
using Azure;
using Cinema.Application.DTO.MovieDTOs;
using Cinema.Application.Helpers.Interfaces;
using Cinema.Application.Interfaces;
using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using System.Net;
using System.Text.Json;

namespace Cinema.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        private readonly IResponses _responses;
        private readonly TmdbService _tmdbService;

        public MovieService(IResponses responses, IUnitOfWork unitOfWork, IMapper mapper, TmdbService tmdbService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _responses = responses;
            _tmdbService = tmdbService;
        }

        public async Task<IBaseResponse<List<GetMovieDTO>>> GetAllMoviesAsync()
        {
            try
            {
                var movies = await _unitOfWork.Movie.GetAllAsync();

                if (movies == null || movies.Count == 0)
                    return _responses.CreateBaseBadRequest<List<GetMovieDTO>>("No movies found.");

                var moviesDto = _mapper.Map<List<GetMovieDTO>>(movies);
                return _responses.CreateBaseOk(moviesDto, moviesDto.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<List<GetMovieDTO>>(ex.Message);
            }
        }

        public async Task<IBaseResponse<GetMovieDTO>> GetMovieByIdAsync(int id)
        {
            try
            {
                var movie = await _unitOfWork.Movie.GetByIdAsync(id);

                if (movie == null)
                    return _responses.CreateBaseNotFound<GetMovieDTO>($"Movie with id {id} not found.");

                var movieDto = _mapper.Map<GetMovieDTO>(movie);
                return _responses.CreateBaseOk(movieDto, 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<GetMovieDTO>(ex.Message);
            }
        }

        public async Task<IBaseResponse<string>> AddMovieAsync(AddMovieDTO movieDto)
        {
            try
            {
                var movie = _mapper.Map<MovieEntity>(movieDto);

                await _unitOfWork.Movie.AddAsync(movie);
                await _unitOfWork.CompleteAsync();

                return _responses.CreateBaseOk("Movie added successfully.", 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<string>(ex.Message);
            }
        }

        public async Task<IBaseResponse<string>> UpdateMovieAsync(int id, UpdateMovieDTO movieDto)
        {
            try
            {
                var existingMovie = await _unitOfWork.Movie.GetByIdAsync(id);

                if (existingMovie == null)
                    return _responses.CreateBaseNotFound<string>($"Movie with id {id} not found.");

                _mapper.Map(movieDto, existingMovie);

                await _unitOfWork.CompleteAsync();

                return _responses.CreateBaseOk("Movie updated successfully.", 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<string>(ex.Message);
            }
        }

        public async Task<IBaseResponse<string>> DeleteMovieAsync(int id)
        {
            try
            {
                await _unitOfWork.Movie.DeleteByIdAsync(id);
                await _unitOfWork.CompleteAsync();

                return _responses.CreateBaseOk("Movie deleted successfully.", 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<string>(ex.Message);
            }
        }

        public async Task<IBaseResponse<List<GetMovieDTO>>> GetTopRatedMoviesAsync(int take)
        {
            try
            {
                var movies = await _unitOfWork.Movie.GetTopRatedAsync(take);

                if (movies == null || movies.Count == 0)
                    return _responses.CreateBaseBadRequest<List<GetMovieDTO>>("No top-rated movies found.");

                var moviesDto = _mapper.Map<List<GetMovieDTO>>(movies);
                return _responses.CreateBaseOk(moviesDto, moviesDto.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<List<GetMovieDTO>>(ex.Message);
            }
        }

        /// <summary>
        /// Додати фільм з TMDB за SearchId.
        /// </summary>
        public async Task<IBaseResponse<string>> AddMovieFromTmdbAsync(int searchId)
        {
            try
            {
                var existingMovie = await _unitOfWork.Movie.GetBySearchIdAsync(searchId);
                if (existingMovie != null)
                {
                    return _responses.CreateBaseConflict<string>($"Movie with SearchId {searchId} already exists in the database.");
                }

                var tmdbMovie = await _tmdbService.GetMovieByIdAsync(searchId);
                if (tmdbMovie == null)
                {
                    return _responses.CreateBaseNotFound<string>($"Movie with SearchId {searchId} not found in TMDB.");
                }

                var movieEntity = tmdbMovie;

                await _unitOfWork.Movie.AddAsync(movieEntity);
                await _unitOfWork.CompleteAsync();

                return _responses.CreateBaseOk("Movie added successfully from TMDB.", 1);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<string>(ex.Message);
            }
        }
    }
}
