using EC.Services.ProductAPI.Data.Abstract;
using EC.Services.ProductAPI.Entities;
using EC.Services.ProductAPI.Settings.Abstract;
using MongoDB.Driver;

namespace EC.Services.ProductAPI.Data.Concrete
{
    public class ProductContext:IProductContext
    {
        public ProductContext(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Products = database.GetCollection<Product>(settings.ProductsCollection);
            ProductVariants = database.GetCollection<ProductVariant>(settings.ProductVariantsCollection);
            Variants = database.GetCollection<Variant>(settings.VariantsCollection);
            Stocks = database.GetCollection<Stock>(settings.StocksCollection);

        }
        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<Variant> Variants { get; }
        public IMongoCollection<ProductVariant> ProductVariants { get; }
        public IMongoCollection<Stock> Stocks { get; }
    }
}
