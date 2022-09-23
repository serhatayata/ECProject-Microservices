using Core.Entities;

namespace EC.Services.PhotoStockAPI.Dtos
{
    public class PhotoDto:IDto
    {
        public int Id { get; set; }
        public int PhotoType { get; set; }
        public int EntityId { get; set; }
        public string Url { get; set; }
    }
}
