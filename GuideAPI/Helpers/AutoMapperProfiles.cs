using AutoMapper;
using GuideAPI.Dtos;
using GuideAPI.Models;

namespace GuideAPI.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<City, CityForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                });


            CreateMap<City, CityForDetailDto>();
        }
    }
}
