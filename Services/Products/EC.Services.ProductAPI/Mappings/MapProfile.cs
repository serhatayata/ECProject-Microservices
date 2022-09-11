using AutoMapper;
using EC.Services.ProductAPI.Dtos.ProductDtos;
using EC.Services.ProductAPI.Dtos.ProductVariantDtos;
using EC.Services.ProductAPI.Dtos.VariantDtos;
using EC.Services.ProductAPI.Entities;

namespace EC.Services.ProductAPI.Mappings
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            #region Product
            CreateMap<Product, ProductAddDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap()
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Link, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore());
            CreateMap<Product, ProductDto>().ReverseMap();
            #endregion
            #region Variant
            CreateMap<Variant, VariantAddDto>().ReverseMap();
            CreateMap<Variant, VariantUpdateDto>().ReverseMap();
            CreateMap<Variant, VariantDto>().ReverseMap();
            #endregion
            #region ProductVariant
            CreateMap<ProductVariant, ProductVariantDto>().ReverseMap();
            CreateMap<ProductVariant, ProductVariantGetDto>().ReverseMap();
            CreateMap<ProductVariant, ProductVariantDeleteDto>().ReverseMap();
            #endregion
        }
    }
}
