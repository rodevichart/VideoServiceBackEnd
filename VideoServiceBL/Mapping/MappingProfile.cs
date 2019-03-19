using AutoMapper;
using VideoServiceBL.DTOs;
using VideoServiceBL.DTOs.MoviesDtos;
using VideoServiceBL.DTOs.RentalsDtos;
using VideoServiceBL.DTOs.UsersDtos;
using VideoServiceDAL.Models;

namespace VideoServiceBL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(QueryResult<>), typeof(QueryResultDto<>));

            CreateMap<Movie, MovieDto>();
            //.ForMember(dest => dest.GenreName,
            //            opt => opt.MapFrom(m => m.Genre.Name));
            CreateMap<MovieDataDto, Movie>();

            CreateMap<Movie, MovieDto>();
            CreateMap<Genre, GenreDto>();

            CreateMap<MovieDto, Movie>();
            CreateMap<GenreDto, Genre>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Rental, RentalDto>();
            CreateMap<RentalDto, Rental>();

            CreateMap<AddRentalDto, Rental>();
        }
    }
}
