using EC.Services.DiscountAPI.Data.Abstract.Dapper;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Data.Concrete.Dapper
{
    public class CampaignUserRepository : ICampaignUserRepository
    {
        public Task<bool> AnyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<CampaignUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CampaignUser> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
