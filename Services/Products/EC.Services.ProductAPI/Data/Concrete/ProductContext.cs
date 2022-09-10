using EC.Services.ProductAPI.Data.Abstract;
using EC.Services.ProductAPI.Entities;
using MongoDB.Driver;

namespace EC.Services.ProductAPI.Data.Concrete
{
    public class ProductContext:IProductContext
    {
        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<Variant> Variants { get; }
        public IMongoCollection<ProductVariant> ProductVariants { get; }
        public IMongoCollection<Stock> Stocks { get; }
    }
}
