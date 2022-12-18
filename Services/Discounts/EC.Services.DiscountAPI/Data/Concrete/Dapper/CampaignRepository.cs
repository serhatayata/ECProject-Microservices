using EC.Services.DiscountAPI.Data.Abstract.Dapper;
using EC.Services.DiscountAPI.Data.Contexts;
using EC.Services.DiscountAPI.Entities;
using Microsoft.Data.SqlClient;
using MongoDB.Driver.Core.Configuration;
using System.Data;

namespace EC.Services.DiscountAPI.Data.Concrete.Dapper
{
    public class CampaignRepository:ICampaignRepository
    {
        private readonly DiscountDbDapperContext _context;

        public CampaignRepository(DiscountDbDapperContext context)
        {
            _context = context;
        }

        #region AnyAsync
        public async Task<bool> AnyAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetByIdAsync
        public Task<Campaign> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllAsync
        public async Task<List<Campaign>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<List<Campaign>> GetAllPagingAsync(int page = 1, int pageSize = 8)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region MyRegion
        public async Task<List<Campaign>> GetAllBetweenDatesByCreatedTimeAsync(DateTime bTime, DateTime eTime)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllBetweenDatesByExpirationTimeAsync
        public async Task<List<Campaign>> GetAllBetweenDatesByExpirationTimeAsync(DateTime bTime, DateTime eTime)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllByCampaignTypeAsync
        public async Task<List<Campaign>> GetAllByCampaignTypeAsync(int campaignType)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllByProductIdAsync
        public async Task<List<Campaign>> GetAllByProductIdAsync(int productId)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllBySponsorAsync
        public async Task<List<Campaign>> GetAllBySponsorAsync(string sponsor)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
