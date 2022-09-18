using Core.Entities;

namespace EC.Services.PhotoStockAPI.Dtos
{
    public class PhotoGetAllByTypeAndEntityIdDto:IDto
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int EntityId { get; set; }
        public string Url { get; set; }
    }
}
