using Core.Entities;

namespace EC.Services.PhotoStockAPI.Dtos
{
    public class PhotoDeleteByUrlDto:IDto
    {
        public string Url { get; set; }
    }
}
