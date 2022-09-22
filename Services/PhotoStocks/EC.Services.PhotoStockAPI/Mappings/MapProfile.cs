using AutoMapper;
using EC.Services.PhotoStockAPI.Dtos;
using EC.Services.PhotoStockAPI.Entities;

namespace EC.Services.PhotoStockAPI.Mappings
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            #region Photo
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<Photo, PhotoAddDto>().ReverseMap();
            CreateMap<Photo, PhotoDeleteDto>().ReverseMap();
            #endregion
        }
    }
}
