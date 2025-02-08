using AutoMapper;
using Azure;
using Cinema.Application.DTO.MovieDTOs;
using Cinema.Application.DTO.SessionDTOs;
using Cinema.Application.Helpers.Interfaces;
using Cinema.Application.Interfaces;
using Cinema.Domain.Entities;
using Cinema.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Cinema.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 
        private readonly IResponses _responses;
        private readonly TmdbService _tmdbService;
        private readonly IDistributedCache _cache;
        private readonly string _cacheKey = "movies_cache";
        private readonly int _cacheExpirationTime = 10;

        public MovieService(IResponses responses, IUnitOfWork unitOfWork, IMapper mapper, TmdbService tmdbService, IDistributedCache cache)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _responses = responses;
            _tmdbService = tmdbService;
            _cache = cache;
        }

        public async Task<IBaseResponse<List<GetMovieDTO>>> GetAllMoviesAsync()
        {
            try
            {
                string serializedMovies;
                List<MovieEntity> movies;

                var cachedMovies = await _cache.GetAsync(_cacheKey);
                if (cachedMovies != null)
                {
                    serializedMovies = Encoding.UTF8.GetString(cachedMovies);
                    movies = JsonConvert.DeserializeObject<List<MovieEntity>>(serializedMovies);
                }
                else
                {
                    movies = await _unitOfWork.Movie.GetAllAsync();

                    if (movies != null && movies.Count > 0)
                    {
                        serializedMovies = JsonConvert.SerializeObject(movies, new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });

                        cachedMovies = Encoding.UTF8.GetBytes(serializedMovies);
                        await _cache.SetAsync(_cacheKey, cachedMovies, new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(_cacheExpirationTime)));
                    }
                }

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

        public async Task<IBaseResponse<List<GetMovieDTO>>> GetFormattedMovies() 
        {
            try {
                var movies = await _unitOfWork.Movie.GetAllAsync();

                if (movies == null || movies.Count == 0)
                    return _responses.CreateBaseBadRequest<List<GetMovieDTO>>("No movies found.");

                var moviesDto = _mapper.Map<List<GetMovieDTO>>(movies);

                foreach (var movie in moviesDto) {
                    var sessions = await _unitOfWork.Session.GetByMovieIdAsync(movie.Id);
                    movie.Sessions = _mapper.Map<List<GetSessionDTO>>(sessions);
                }

                return _responses.CreateBaseOk(moviesDto, moviesDto.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<List<GetMovieDTO>>(ex.Message);
            }
        }

        public async Task<IBaseResponse<GetMovieDTO>> GetMovieDataByIdAsync(int id)
        {
            try {
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
        public async Task<IBaseResponse<List<GetMovieDTO>>> GetAllMoviesWithPaginationAsync(int take, int skip, string sortBy, bool ascending)
        {
            if (take <= 0 || skip < 0)
            {
                return _responses.CreateBaseBadRequest<List<GetMovieDTO>>("Invalid pagination parameters.");
            }

            var validSortFields = new List<string> { "Title", "Release_date", "Rating" };
            if (!validSortFields.Contains(sortBy))
            {
                return _responses.CreateBaseBadRequest<List<GetMovieDTO>>($"Invalid sort field. Valid fields are: {string.Join(", ", validSortFields)}.");
            }

            try
            {
                var movies = await _unitOfWork.Movie.GetAllWithPaginationAsync(take, skip, sortBy, ascending);

                if (movies == null || movies.Count == 0)
                {
                    return _responses.CreateBaseBadRequest<List<GetMovieDTO>>("No movies found.");
                }

                var moviesDto = _mapper.Map<List<GetMovieDTO>>(movies);
                return _responses.CreateBaseOk(moviesDto, moviesDto.Count);
            }
            catch (Exception ex)
            {
                return _responses.CreateBaseServerError<List<GetMovieDTO>>(ex.Message);
            }
        }

    }
}
