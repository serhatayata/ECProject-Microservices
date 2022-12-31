using Core.Entities;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Dtos.Discount
{
    public class DiscountAddDto : IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DiscountTypes DiscountType { get; set; }
        public string Sponsor { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
