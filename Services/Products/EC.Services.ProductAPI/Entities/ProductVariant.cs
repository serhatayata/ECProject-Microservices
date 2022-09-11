using Core.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EC.Services.ProductAPI.Entities
{
    public class ProductVariant:IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string VariantId { get; set; }
    }
}
