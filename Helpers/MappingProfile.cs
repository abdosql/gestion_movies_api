using AutoMapper;
using mastering_.NET_API.Dtos;

namespace mastering_.NET_API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateGenreDto, Genre>()
                .ForMember(dest => dest.Name, src => src.MapFrom(src => src.NameGenre))
                .ReverseMap(); // hadi bach ymapi fles deux sense
            
            CreateMap<UpdateGenreDto, Genre>();
        }
    }
}
