using EC.Services.ProductAPI.Data.SeedData;
using EC.Services.ProductAPI.Entities;
using EC.Services.ProductAPI.Settings.Abstract;
using MongoDB.Driver;

namespace EC.Services.ProductAPI.Extensions
{
    public static class SeedDataExtensions
    {
        private static IMongoDatabase database;
       
        public static void Configure(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);
        }

        public static void AddSeedData()
        {
            var client = new MongoClient(settings.ConnectionString);
        }
    }
}
