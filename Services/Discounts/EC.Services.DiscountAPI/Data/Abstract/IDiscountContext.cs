using EC.Services.DiscountAPI.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EC.Services.DiscountAPI.Data.Abstract
{
    public interface IDiscountContext
    {
        IMongoCollection<Discount> Discounts { get; }
        IMongoQueryable<Discount> DiscountsAsQueryable { get; }
        IMongoCollection<Campaign> Campaigns { get; }
        IMongoQueryable<Campaign> CampaignsAsQueryable { get; }

    }
}
