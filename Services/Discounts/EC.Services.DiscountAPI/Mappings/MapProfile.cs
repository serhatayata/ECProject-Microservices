﻿using AutoMapper;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.Discount;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Mappings
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            #region Discount
            CreateMap<Discount, DiscountAddDto>().ReverseMap();
            CreateMap<Discount, DiscountUpdateDto>().ReverseMap()
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.CDate, opt => opt.Ignore())
                .ForMember(x => x.Code, opt => opt.Ignore());
            CreateMap<Discount, DiscountDto>().ReverseMap();
            #endregion
            #region Campaign
            CreateMap<Campaign, CampaignAddDto>().ReverseMap();
            CreateMap<Campaign, CampaignUpdateDto>().ReverseMap()
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.CDate, opt => opt.Ignore())
                .ForMember(x => x.Products, opt => opt.Ignore());
            CreateMap<Campaign, CampaignDto>().ReverseMap();
            #endregion
        }
    }
}