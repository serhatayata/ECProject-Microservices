using Core.DataAccess;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Data.Abstract.EntityFramework
{
    public interface ICampaignDal : IEntityRepository<Campaign>
    {
    }
}
