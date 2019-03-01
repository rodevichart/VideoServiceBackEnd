using AutoMapper;
using VideoServiceBL.DTOs;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceBL.DTOs.UsersDtos;
using VideoServiceDAL.Models;

namespace VideoServiceBL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, ListOfMoviesDto>()
                .ForMember(dest => dest.GenreName,
                            opt => opt.MapFrom(m => m.Genre.Name));


            CreateMap<Movie, MovieDto>();
            CreateMap<Genre, GenreDto>();

            CreateMap<MovieDto, Movie>();
            CreateMap<GenreDto, Genre>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<UserDto, AuthenticateUserDto>();
            CreateMap<AuthenticateUserDto, UserDto>();

            CreateMap<UserDto, AuthenticateUserDto>();
            CreateMap<AuthenticateUserDto, UserDto>();
        }
    }
}
