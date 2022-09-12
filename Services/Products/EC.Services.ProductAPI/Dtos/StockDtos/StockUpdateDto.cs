using Core.Entities;

namespace EC.Services.ProductAPI.Dtos.StockDtos
{
    public class StockUpdateDto:IDto
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
