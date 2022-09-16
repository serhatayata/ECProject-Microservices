using AutoMapper;
using EC.Services.CategoryAPI.Dtos.CategoryDtos;
using EC.Services.CategoryAPI.Entities;

namespace EC.Services.CategoryAPI.Mappings
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            #region Category
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryAddDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap()
                .ForMember(x => x.Link, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore());
            #endregion
        }
    }
}
