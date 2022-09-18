using Core.Entities;

namespace EC.Services.PhotoStockAPI.Dtos
{
    public class PhotoGetAllByTypeDto:IDto
    {
        public int Type { get; set; }
    }
}
