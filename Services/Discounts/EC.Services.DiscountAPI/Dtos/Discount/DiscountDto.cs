using Core.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace EC.Services.DiscountAPI.Dtos.Discount
{
    public class DiscountDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DiscountTypes DiscountType { get; set; }
        public string Sponsor { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public DateTime CDate { get; set; }
        public DateTime UDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
