using Core.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EC.Services.DiscountAPI.Entities
{
    public class Campaign
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int CampaignType { get; set; }
        public int Rate { get; set; }
        public bool Status { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CDate { get; set; }
        public List<string> Products { get; set; }
    }

    public enum CampaignTypes
    {
        Price = 1,
        Percentage = 2,
        OverPrice=3,
        OverPercentage=4
    }
}
