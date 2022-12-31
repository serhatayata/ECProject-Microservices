using Core.Entities;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Dtos.Discount
{
    public class DiscountIdDto:IDto
    {
        public int Id { get; set; }
        public DiscountStatus Status { get; set; } = DiscountStatus.Active;
    }
}
