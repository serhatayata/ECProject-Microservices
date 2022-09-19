using Core.Entities;

namespace EC.Services.PhotoStockAPI.Dtos
{
    public class PhotoDto:IDto
    {
        public int Id { get; set; }
        public IFormFile Photo { get; set; }
        public int Type { get; set; }
        public int EntityId { get; set; }
        public string Url { get; set; }
    }
}
