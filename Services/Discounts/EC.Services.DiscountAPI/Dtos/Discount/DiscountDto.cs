using Core.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace EC.Services.DiscountAPI.Dtos.Discount
{
    public class DiscountDto : IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int DiscountType { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public DateTime CDate { get; set; }
    }
}
