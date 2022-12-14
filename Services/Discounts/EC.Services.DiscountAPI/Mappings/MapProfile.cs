using AutoMapper;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.CampaignProduct;
using EC.Services.DiscountAPI.Dtos.CampaignUser;
using EC.Services.DiscountAPI.Dtos.Discount;
using EC.Services.DiscountAPI.Entities;
using EC.Services.DiscountAPI.Validations.CampaignValidations;

namespace EC.Services.DiscountAPI.Mappings
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            #region Discount
            CreateMap<Discount, DiscountAddDto>().ReverseMap();
            CreateMap<DiscountDto, DiscountAddDto>().ReverseMap();
            CreateMap<Discount, DiscountUpdateDto>().ReverseMap()
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.CDate, opt => opt.Ignore());
            CreateMap<Discount, DiscountDto>().ReverseMap();
            CreateMap<DiscountDto, DiscountUpdateDto>().ReverseMap();
            #endregion
            #region Campaign
            CreateMap<Campaign, CampaignAddDto>().ReverseMap();
            CreateMap<Campaign, CampaignUpdateDto>().ReverseMap()
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.CDate, opt => opt.Ignore())
                .ForMember(x => x.UDate, opt => opt.Ignore());
            CreateMap<Campaign, CampaignDto>().ReverseMap();
            CreateMap<CampaignDto, CampaignUpdateDto>().ReverseMap();
            #endregion
            #region CampaignProduct
            CreateMap<CampaignProduct, CampaignAddProductDto>().ReverseMap();
            CreateMap<CampaignProduct, CampaignProductDto>().ReverseMap();
            #endregion
            #region CampaignProduct
            CreateMap<CampaignUser, CampaignUserAddDto>().ReverseMap();
            CreateMap<CampaignUser, CampaignUserDto>().ReverseMap();
            CreateMap<CampaignUser, CampaignUserDeleteDto>().ReverseMap();
            #endregion
        }
    }
}
