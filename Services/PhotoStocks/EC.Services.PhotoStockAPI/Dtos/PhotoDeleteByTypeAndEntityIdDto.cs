using Core.Entities;

namespace EC.Services.PhotoStockAPI.Dtos
{
    public class PhotoDeleteByTypeAndEntityIdDto:IDto
    {
        public int PhotoType { get; set; }
        public int EntityId { get; set; }
    }
}
