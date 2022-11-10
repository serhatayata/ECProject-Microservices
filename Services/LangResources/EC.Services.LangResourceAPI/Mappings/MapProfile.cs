using AutoMapper;
using EC.Services.LangResourceAPI.Dtos.LangDtos;
using EC.Services.LangResourceAPI.Dtos.LangResourceDtos;
using EC.Services.LangResourceAPI.Entities;

namespace EC.Services.LangResourceAPI.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region LangResource
            CreateMap<LangResource, LangResourceDto>().ReverseMap();
            CreateMap<LangResource, LangResourceUpdateDto>().ReverseMap();
            CreateMap<LangResource, LangResourceAddDto>().ReverseMap();

            CreateMap<Lang, LangDto>().ReverseMap();
            CreateMap<Lang, LangUpdateDto>().ReverseMap();
            CreateMap<Lang, LangAddDto>().ReverseMap();

            #endregion
        }
    }
}
