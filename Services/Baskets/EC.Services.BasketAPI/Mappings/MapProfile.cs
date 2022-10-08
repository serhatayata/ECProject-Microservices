using AutoMapper;
using EC.Services.BasketAPI.Dtos;

namespace EC.Services.BasketAPI.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region Basket
            CreateMap<BasketDto, BasketSaveOrUpdateDto>().ReverseMap();
            #endregion
        }
    }
}
