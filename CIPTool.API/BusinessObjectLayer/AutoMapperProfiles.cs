using AutoMapper;
using BusinessObjectLayer.Dtos;
using BusinessObjectLayer.Entities;

namespace BusinessObjectLayer
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<SimplifiedUserDto, Associate>();
            CreateMap<Associate, SimplifiedUserDto>();
        }
    }
}
