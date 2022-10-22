using Core.Entities;
using Core.Extensions;
using EC.Services.DiscountAPI.Data.Concrete;
using EC.Services.DiscountAPI.Entities;
using EC.Services.DiscountAPI.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EC.Services.DiscountAPI.Extensions
{
    public static class SeedDataExtensions
    {
        private static IMongoDatabase _database;
        private static DiscountDatabaseSettings _settings;
        private static MongoClient _client;

        public static void Configure(DiscountDatabaseSettings settings)
        {
            _settings = settings;
            _client = new MongoClient(_settings.ConnectionString);
            _database = _client.GetDatabase(_settings.DatabaseName);
        }

        public async static Task AddSeedData()
        {
            var discounts = _database.GetCollection<Discount>(_settings.DiscountsCollection);
            var campaigns = _database.GetCollection<Campaign>(_settings.CampaignsCollection);

            bool existsDiscount= discounts.Find(x => true).Any();
            bool existsCampaign = campaigns.Find(x => true).Any();

            if (!existsDiscount)
            {
                await discounts.InsertManyAsync(GetSeedDataDiscounts());
            }
            if (!existsCampaign)
            {
                await campaigns.InsertManyAsync(GetSeedDataCampaigns());
            }
        }

        #region GetSeedDataDiscounts
        private static List<Discount> GetSeedDataDiscounts()
        {
            return new List<Discount>()
            {
                new()
                {
                    Name="discount_1",
                    Rate=10,
                    Code="AAABBB",
                    DiscountType=(int)DiscountTypes.Percentage,
                    CDate=DateTime.Now,
                    Status=true
                },
                new()
                {
                    Name="discount_2",
                    Rate=20,
                    Code="AAACCC",
                    DiscountType=(int)DiscountTypes.Price,
                    CDate=DateTime.Now,
                    Status=true
                },
                new()
                {
                    Name="discount_3",
                    Rate=30,
                    Code="DDDBBB",
                    DiscountType=(int)DiscountTypes.Price,
                    CDate=DateTime.Now,
                    Status=true
                }
            };
        }
        #endregion
        #region GetSeedDataCampaigns
        private static List<Campaign> GetSeedDataCampaigns()
        {
            return new List<Campaign>()
            {
                new()
                {
                    Name="campaign_1",
                    Products=new List<string>(),
                    CDate=DateTime.Now,
                    Status=true,
                    CampaignType=(int)CampaignTypes.Percentage,
                    Rate=20
                },
                new()
                {
                    Name="campaign_1",
                    Products=new List<string>(),
                    CDate=DateTime.Now,
                    Status=true,
                    CampaignType=(int)CampaignTypes.Price,
                    Rate=30
                },
                new()
                {
                    Name="campaign_1",
                    Products=new List<string>(),
                    CDate=DateTime.Now,
                    Status=true,
                    CampaignType=(int)CampaignTypes.OverPercentage,
                    Rate=40
                },
                new()
                {
                    Name="campaign_1",
                    Products=new List<string>(),
                    CDate=DateTime.Now,
                    Status=true,
                    CampaignType=(int)CampaignTypes.OverPrice,
                    Rate=50
                }

            };
        }
        #endregion
    }


}
