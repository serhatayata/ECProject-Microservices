using Core.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EC.Services.DiscountAPI.Entities
{
    public class Discount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int DiscountType { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CDate { get; set; }

    }

    public enum DiscountTypes
    {
        Price=1,
        Percentage=2
    }
}