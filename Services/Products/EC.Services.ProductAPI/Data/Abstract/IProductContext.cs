using EC.Services.ProductAPI.Entities;
using MongoDB.Driver;

namespace EC.Services.ProductAPI.Data.Abstract
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<Variant> Variants { get; }
        IMongoCollection<ProductVariant> ProductVariants { get; }
        IMongoCollection<Stock> Stocks { get; }
    }
}
