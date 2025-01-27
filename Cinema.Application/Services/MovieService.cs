using AutoMapper;
using Cinema.Application.DTO.MovieDTOs;
using Cinema.Application.Interfaces;
using Cinema.Domain.Entities;

namespace Cinema.Application.Services
{
    class MovieService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(AddMovieDTO movieDto)
        {
            var movie = _mapper.Map<MovieEntity>(movieDto);
            await _unitOfWork.Movie.AddAsync(movie);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _unitOfWork.Movie.DeleteByIdAsync(id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<List<GetMovieDTO>> GetAllAsync()
        {
            var movies = await _unitOfWork.Movie.GetAllAsync();
            return _mapper.Map<List<GetMovieDTO>>(movies);
        }

        public async Task<GetMovieDTO> GetByIdAsync(int id)
        {
            var movie = await _unitOfWork.Movie.GetByIdAsync(id);
            return _mapper.Map<GetMovieDTO>(movie);
        }

        public async Task UpdateAsync(UpdateMovieDTO movieDto)
        {
            var movie = await _unitOfWork.Movie.GetByIdAsync(movieDto.SearchId);
            _mapper.Map(movieDto, movie);
            await _unitOfWork.CompleteAsync();
        }
    }
}
