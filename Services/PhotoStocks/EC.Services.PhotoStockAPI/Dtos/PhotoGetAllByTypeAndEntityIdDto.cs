using Core.Entities;

namespace EC.Services.PhotoStockAPI.Dtos
{
    public class PhotoGetAllByTypeAndEntityIdDto:IDto
    {
        public int Type { get; set; }
        public int EntityId { get; set; }
    }
}
