using Core.Extensions;
using EC.Services.ProductAPI.Entities;
using EC.Services.ProductAPI.Settings.Concrete;
using MongoDB.Driver;

namespace EC.Services.ProductAPI.Extensions
{
    public static class SeedDataExtensions
    {
        private static IMongoDatabase _database;
        private static ProductDatabaseSettings _settings;
        private static MongoClient _client;
       
        public static void Configure(ProductDatabaseSettings settings)
        {
            _settings = settings;
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
        }

        public async static Task AddSeedData()
        {
            var products = _database.GetCollection<Product>(_settings.ProductsCollection);
            var productVariants = _database.GetCollection<ProductVariant>(_settings.ProductVariantsCollection);
            var variants = _database.GetCollection<Variant>(_settings.VariantsCollection);
            var stocks = _database.GetCollection<Stock>(_settings.StocksCollection);

            bool existsProduct = products.Find(x => true).Any();
            bool existsProductVariant = productVariants.Find(x => true).Any();
            bool existsVariant = variants.Find(x => true).Any();
            bool existsStock = stocks.Find(x => true).Any();

            if (!existsProduct)
            {
                await products.InsertManyAsync(GetSeedDataProducts());
            }
            if (!existsVariant)
            {
                await variants.InsertManyAsync(GetSeedDataVariants());
            }
            if (!existsProductVariant)
            {
                await productVariants.InsertManyAsync(GetSeedDataProductVariants());
            }
            if (!existsStock)
            {
                await stocks.InsertManyAsync(GetSeedDataStocks());
            }
        }

        #region GetSeedDataProducts
        private static List<Product> GetSeedDataProducts()
        {
            return new List<Product>()
            {
                new()
                {
                    Name="product_1",
                    CategoryId=1,
                    Line=1,
                    Link=SeoLinkExtensions.GenerateSlug("product-1"),
                    Price=500.00M,
                    Status=true,
                    CreatedAt=DateTime.Now
                },
                new()
                {
                    Name="product_2",
                    CategoryId=1,
                    Line=2,
                    Link=SeoLinkExtensions.GenerateSlug("product-2"),
                    Price=400.00M,
                    Status=true,
                    CreatedAt=DateTime.Now
                },
                new()
                {
                    Name="product_3",
                    CategoryId=1,
                    Line=3,
                    Link=SeoLinkExtensions.GenerateSlug("product-3"),
                    Price=800.00M,
                    Status=true,
                    CreatedAt=DateTime.Now
                },
            };
        }
        #endregion
        #region GetSeedDataVariants
        private static List<Variant> GetSeedDataVariants()
        {
            return new List<Variant>()
            {
                new()
                {
                    Name="variant_1"
                },
                new()
                {
                    Name="variant_2"
                },
                new()
                {
                    Name="variant_3"
                },
            };
        }
        #endregion
        #region GetSeedDataProductVariants
        private static List<ProductVariant> GetSeedDataProductVariants()
        {
            var allProducts = _database.GetCollection<Product>(_settings.ProductsCollection)?.Find(x=>true)?.ToList();
            var allVariants = _database.GetCollection<Variant>(_settings.VariantsCollection)?.Find(x=>true)?.ToList();
            return new List<ProductVariant>()
            {
                
                new()
                {
                    ProductId=allProducts[0].Id,
                    VariantId=allVariants[0].Id
                },
                new()
                {
                    ProductId=allProducts[1].Id,
                    VariantId=allVariants[1].Id
                },
                new()
                {
                    ProductId=allProducts[2].Id,
                    VariantId=allVariants[2].Id
                }
            };
        }
        #endregion
        #region GetSeedDataStocks
        private static List<Stock> GetSeedDataStocks()
        {
            var allProducts = _database.GetCollection<Product>(_settings.ProductsCollection)?.Find(x => true)?.ToList();
            return new List<Stock>()
            {
                new()
                {
                    ProductId=allProducts[0].Id,
                    Quantity=100
                },
                new()
                {
                    ProductId=allProducts[1].Id,
                    Quantity=200
                },
                new()
                {
                    ProductId=allProducts[2].Id,
                    Quantity=300
                }
            };
        }
        #endregion
    }
}
