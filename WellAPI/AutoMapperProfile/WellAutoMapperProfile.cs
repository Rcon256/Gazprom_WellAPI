using WellAPI.DTO;
using WellAPI.Models;

namespace WellAPI.AutoMapperProfile
{
    public class WellAutoMapperProfile : AutoMapper.Profile
    {
        public WellAutoMapperProfile()
        {
            CreateMap<Well, WellDTO>()
                .ReverseMap();
        }
    }
}
