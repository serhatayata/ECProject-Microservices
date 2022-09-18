using Core.Entities;

namespace EC.Services.PhotoStockAPI.Dtos
{
    public class PhotoDeleteDto:IDto
    {
        public int Type { get; set; }
        public int EntityId { get; set; }
    }
}
