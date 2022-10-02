using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Discount
{
    public class DiscountUpdateDto : IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public int DiscountType { get; set; }

    }
}
