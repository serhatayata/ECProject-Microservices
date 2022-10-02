using EC.Services.DiscountAPI.Data.Abstract;
using EC.Services.DiscountAPI.Entities;
using EC.Services.DiscountAPI.Settings;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace EC.Services.DiscountAPI.Data.Concrete
{
    public class DiscountContext:IDiscountContext
    {
        private readonly DiscountDatabaseSettings _settings;
        public DiscountContext(IOptions<DiscountDatabaseSettings> settings)
        {
            _settings = settings.Value;

            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);

            Discounts = database.GetCollection<Discount>(_settings.DiscountsCollection);
            Campaigns = database.GetCollection<Campaign>(_settings.CampaignsCollection);

            //Queryable
            DiscountsAsQueryable = database.GetCollection<Discount>(_settings.DiscountsCollection).AsQueryable<Discount>();
            CampaignsAsQueryable = database.GetCollection<Campaign>(_settings.CampaignsCollection).AsQueryable<Campaign>();

        }
        public IMongoCollection<Discount> Discounts { get; }
        public IMongoQueryable<Discount> DiscountsAsQueryable { get; }
        public IMongoCollection<Campaign> Campaigns { get; }
        public IMongoQueryable<Campaign> CampaignsAsQueryable { get; }
    }
}
