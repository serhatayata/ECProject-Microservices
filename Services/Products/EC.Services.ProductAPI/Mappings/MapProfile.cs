using AutoMapper;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Entities;

namespace EC.Services.ProductAPI.Mappings
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductAddDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap()
                .ForMember(x=>x.Status, opt=>opt.Ignore())
                .ForMember(x=>x.Link, opt=>opt.Ignore())
                .ForMember(x=>x.CreatedAt, opt=>opt.Ignore());
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
