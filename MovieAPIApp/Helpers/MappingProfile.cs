using AutoMapper;
using MovieAPIApp.Dtos;

namespace MovieAPIApp.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDetailsDto>();
            CreateMap<CreateMovieDTO, Movie>().ForMember(src => src.Poster ,opt => opt.Ignore());
        }
    }
}
