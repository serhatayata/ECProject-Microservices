using EC.Services.ProductAPI.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EC.Services.ProductAPI.Data.Abstract
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoQueryable<Product> ProductsAsQueryable { get; }
        IMongoCollection<Variant> Variants { get; }
        IMongoQueryable<Variant> VariantsAsQueryable { get; }
        IMongoCollection<ProductVariant> ProductVariants { get; }
        IMongoQueryable<ProductVariant> ProductVariantsAsQueryable { get; }
        IMongoCollection<Stock> Stocks { get; }
        IMongoQueryable<Stock> StocksAsQueryable { get; }
    }
}
