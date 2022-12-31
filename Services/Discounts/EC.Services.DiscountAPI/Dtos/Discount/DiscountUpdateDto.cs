using Core.Entities;

namespace EC.Services.DiscountAPI.Dtos.Discount
{
    public class DiscountUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DiscountTypes DiscountType { get; set; }
        public string Sponsor { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
    }
}
