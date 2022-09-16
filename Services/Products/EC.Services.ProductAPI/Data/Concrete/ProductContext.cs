using EC.Services.ProductAPI.Data.Abstract;
using EC.Services.ProductAPI.Entities;
using EC.Services.ProductAPI.Settings.Abstract;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EC.Services.ProductAPI.Data.Concrete
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Products = database.GetCollection<Product>(settings.ProductsCollection);
            ProductVariants = database.GetCollection<ProductVariant>(settings.ProductVariantsCollection);
            Variants = database.GetCollection<Variant>(settings.VariantsCollection);
            Stocks = database.GetCollection<Stock>(settings.StocksCollection);
            //Queryable
            ProductsAsQueryable = database.GetCollection<Product>(settings.ProductsCollection).AsQueryable<Product>();
            ProductVariantsAsQueryable = database.GetCollection<ProductVariant>(settings.ProductVariantsCollection).AsQueryable<ProductVariant>();
            VariantsAsQueryable = database.GetCollection<Variant>(settings.VariantsCollection).AsQueryable<Variant>();
            StocksAsQueryable = database.GetCollection<Stock>(settings.StocksCollection).AsQueryable<Stock>();
        }
        public IMongoCollection<Product> Products { get; }
        public IMongoQueryable<Product> ProductsAsQueryable { get; }
        public IMongoCollection<Variant> Variants { get; }
        public IMongoQueryable<Variant> VariantsAsQueryable { get; }
        public IMongoCollection<ProductVariant> ProductVariants { get; }
        public IMongoQueryable<ProductVariant> ProductVariantsAsQueryable { get; }
        public IMongoCollection<Stock> Stocks { get; }
        public IMongoQueryable<Stock> StocksAsQueryable { get; }
    }
}
