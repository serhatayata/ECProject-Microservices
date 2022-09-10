using Core.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace EC.Services.ProductAPI.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public int Line { get; set; }
        public int CategoryId { get; set; }
    }
}
