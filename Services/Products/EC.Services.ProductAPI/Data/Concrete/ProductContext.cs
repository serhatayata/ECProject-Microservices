using EC.Services.ProductAPI.Data.Abstract;
using EC.Services.ProductAPI.Entities;
using EC.Services.ProductAPI.Settings.Concrete;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EC.Services.ProductAPI.Data.Concrete
{
    public class ProductContext : IProductContext
    {
        private readonly ProductDatabaseSettings _settings;
        public ProductContext(IOptions<ProductDatabaseSettings> settings)
        {
            _settings = settings.Value;

            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);

            Products = database.GetCollection<Product>(_settings.ProductsCollection);
            ProductVariants = database.GetCollection<ProductVariant>(_settings.ProductVariantsCollection);
            Variants = database.GetCollection<Variant>(_settings.VariantsCollection);
            Stocks = database.GetCollection<Stock>(_settings.StocksCollection);
            //Queryable
            ProductsAsQueryable = database.GetCollection<Product>(_settings.ProductsCollection).AsQueryable<Product>();
            ProductVariantsAsQueryable = database.GetCollection<ProductVariant>(_settings.ProductVariantsCollection).AsQueryable<ProductVariant>();
            VariantsAsQueryable = database.GetCollection<Variant>(_settings.VariantsCollection).AsQueryable<Variant>();
            StocksAsQueryable = database.GetCollection<Stock>(_settings.StocksCollection).AsQueryable<Stock>();
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
