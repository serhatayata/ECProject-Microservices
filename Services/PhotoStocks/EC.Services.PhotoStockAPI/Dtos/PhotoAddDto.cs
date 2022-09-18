using Core.Entities;

namespace EC.Services.PhotoStockAPI.Dtos
{
    public class PhotoAddDto:IDto
    {
        public int Type { get; set; }
        public int EntityId { get; set; }
        public string Url { get; set; }
    }
}
