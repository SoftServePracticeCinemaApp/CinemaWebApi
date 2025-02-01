﻿using AutoMapper;
using Cinema.Domain.Entities;
using Cinema.Application.DTO.HallEntityDTOs;
using Cinema.Application.DTO.MovieDTOs;
using Cinema.Application.DTO.SessionDTOs;
using Cinema.Application.DTO.TicketDTOs;
using Cinema.Application.Services;

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
            CreateMap<UpdateSessionDTO, SessionEntity>()
            .ForMember(dest => dest.Date, opt => opt.Condition(src => src.Date != default))
            .ForMember(dest => dest.HallId, opt => opt.Condition(src => src.HallId != default)).ReverseMap();

            CreateMap<TicketEntity, GetTicketDTO>().ReverseMap();
            CreateMap<TicketEntity, AddTicketDTO>().ReverseMap();
            CreateMap<TicketEntity, UpdateTicketDTO>().ReverseMap();

            CreateMap<TmdbMovieResponse, MovieEntity>()
                .ForMember(dest => dest.SearchId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate.Value.ToString("yyyy-MM-dd")));
            CreateMap<TmdbMovieResponse, MovieEntity>()
                .ForMember(dest => dest.SearchId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Overview, opt => opt.MapFrom(src => src.Overview))
                .ForMember(dest => dest.PosterPath, opt => opt.MapFrom(src => src.PosterPath != null
                ? $"https://image.tmdb.org/t/p/w500/{src.PosterPath}"
                : string.Empty))
                .ForMember(dest => dest.CinemaRating, opt => opt.MapFrom(src => src.VoteAverage ?? 0.0))
                .AfterMap((src, dest) =>
                {
                    dest.ReleaseDate = src.ReleaseDate.HasValue
                    ? src.ReleaseDate.Value.ToString("yyyy-MM-dd")
                    : "0001-01-01";
                });
        }
    }
}
