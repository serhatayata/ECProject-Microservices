using Core.Entities;
using Dapper;
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
            var sql = "SELECT TOP 1 * FROM Campaigns WHERE Status=@Status";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Campaign>(sql, new { Status = (byte)CampaignStatus.Active });
                if (result.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
        #region GetByIdAsync
        public async Task<Campaign> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Campaigns WHERE Id=@Id AND Status=@Status";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Campaign>(sql, new { Id = id, Status = (byte)CampaignStatus.Active });
                return result;
            }
        }
        #endregion
        #region GetAllAsync
        public async Task<List<Campaign>> GetAllAsync()
        {
            var sql = "SELECT * FROM Campaigns WHERE Status=@Status";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Campaign>(sql, new { Status = (byte)CampaignStatus.Active });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<List<Campaign>> GetAllPagingAsync(int page = 1, int pageSize = 8)
        {
            var sql = "SELECT * FROM Campaigns WHERE Status=@Status ORDER BY Id OFFSET @Page * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Campaign>(sql, new { Page = page - 1, PageSize = pageSize, Status = (byte)CampaignStatus.Active });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllBetweenDatesByCreatedTimeAsync
        public async Task<List<Campaign>> GetAllBetweenDatesByCreatedTimeAsync(DateTime bTime, DateTime eTime)
        {
            var sql = "SELECT * FROM Campaigns WHERE Status=@Status AND CDate BETWEEN @BeginningTime and @EndingTime";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Campaign>(sql, new { BeginningTime = bTime, EndingTime = eTime, Status = (byte)CampaignStatus.Active });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllBetweenDatesByExpirationTimeAsync
        public async Task<List<Campaign>> GetAllBetweenDatesByExpirationTimeAsync(DateTime bTime, DateTime eTime)
        {
            var sql = "SELECT * FROM Campaigns WHERE Status=@Status AND ExpirationDate BETWEEN @BeginningTime and @EndingTime";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Campaign>(sql, new { BeginningTime = bTime, EndingTime = eTime, Status = (byte)CampaignStatus.Active });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllByCampaignTypeAsync
        public async Task<List<Campaign>> GetAllByCampaignTypeAsync(int campaignType)
        {
            var sql = "SELECT * FROM Campaigns WHERE CampaignType=@CampaignType AND Status=@Status";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Campaign>(sql, new { CampaignType = campaignType, Status = (byte)CampaignStatus.Active });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllByProductIdAsync
        public async Task<List<Campaign>> GetAllByProductIdAsync(int productId)
        {
            var sql = "SELECT * FROM Campaigns WHERE ProductId=@ProductId AND Status=@Status";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Campaign>(sql, new { ProductId = productId, Status = (byte)CampaignStatus.Active });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllBySponsorAsync
        public async Task<List<Campaign>> GetAllBySponsorAsync(string sponsor)
        {
            var sql = "SELECT * FROM Campaigns WHERE Sponsor=@Sponsor AND Status=@Status";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Campaign>(sql, new { Sponsor = sponsor, Status = (byte)CampaignStatus.Active });
                return result.ToList();
            }
        }
        #endregion
    }
}
