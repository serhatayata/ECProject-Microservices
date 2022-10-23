using Core.Entities;

namespace EC.Services.PaymentAPI.ApiDtos
{
    public class DiscountApiDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int DiscountType { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }

    }
}
