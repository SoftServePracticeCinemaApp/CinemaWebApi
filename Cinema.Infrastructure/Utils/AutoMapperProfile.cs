using AutoMapper;
using Cinema.Domain.Entities;
using Cinema.Application.DTO.HallEntityDTOs;
using Cinema.Application.DTO.MovieDTOs;
using Cinema.Application.DTO.SessionDTOs;
using Cinema.Application.DTO.TicketDTOs;

namespace Cinema.Infrastructure.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<HallEntity, GetHallDTO>().ReverseMap();
            CreateMap<HallEntity, AddHallDTO>().ReverseMap();
            CreateMap<HallEntity, UpdateHallDTO>().ReverseMap();

            CreateMap<MovieEntity, GetMovieDTO>().ReverseMap();
            CreateMap<MovieEntity, AddMovieDTO>().ReverseMap();
            CreateMap<MovieEntity, UpdateMovieDTO>().ReverseMap();

            CreateMap<SessionEntity, GetSessionDTO>().ReverseMap();
            CreateMap<SessionEntity, AddSessionDTO>().ReverseMap();
            CreateMap<SessionEntity, UpdateSessionDTO>().ReverseMap();

            CreateMap<TicketEntity, GetTicketDTO>().ReverseMap();
            CreateMap<TicketEntity, AddTicketDTO>().ReverseMap();
            CreateMap<TicketEntity, UpdateTicketDTO>().ReverseMap();
        }
    }
}
