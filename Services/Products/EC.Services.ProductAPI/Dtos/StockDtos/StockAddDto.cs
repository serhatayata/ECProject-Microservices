using Core.Entities;

namespace EC.Services.ProductAPI.Dtos.StockDtos
{
    public class StockAddDto:IDto
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
