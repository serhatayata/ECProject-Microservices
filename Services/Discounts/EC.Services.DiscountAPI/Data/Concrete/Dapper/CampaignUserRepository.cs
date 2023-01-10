using Dapper;
using EC.Services.DiscountAPI.Data.Abstract.Dapper;
using EC.Services.DiscountAPI.Data.Contexts;
using EC.Services.DiscountAPI.Dtos.Campaign;
using EC.Services.DiscountAPI.Dtos.CampaignUser;
using EC.Services.DiscountAPI.Entities;
using System.Drawing.Printing;

namespace EC.Services.DiscountAPI.Data.Concrete.Dapper
{
    public class CampaignUserRepository : ICampaignUserRepository
    {
        private readonly DiscountDbDapperContext _context;

        public CampaignUserRepository(DiscountDbDapperContext context)
        {
            _context = context;
        }

        #region AnyAsync
        public async Task<bool> AnyAsync()
        {
            var sql = "SELECT TOP 1 * FROM CampaignUsers";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<CampaignUser>(sql);
                if (result.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
        #region GetByIdAsync
        public async Task<CampaignUser> GetByIdAsync(int id)
        {
            return new CampaignUser();
        }
        #endregion
        #region GetAllAsync
        public async Task<List<CampaignUser>> GetAllAsync()
        {
            var sql = "SELECT * FROM CampaignUsers";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<CampaignUser>(sql);
                return result.ToList();
            }
        }
        #endregion
        #region GetAllByCampaignIdAsync
        public async Task<List<CampaignUser>> GetAllByCampaignIdAsync(int campaignId, bool isUsed)
        {
            var sql = "SELECT * FROM CampaignUsers WHERE CampaignId=@CampaignId AND IsUsed=@IsUsed";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<CampaignUser>(sql, new { CampaignId=campaignId, IsUsed = isUsed });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllByCampaignIdPagingAsync
        public async Task<List<CampaignUser>> GetAllByCampaignIdPagingAsync(CampaignIdPagingDto model)
        {
            var sql = "SELECT * FROM CampaignUsers WHERE IsUsed=@IsUsed AND CampaignId=@CampaignId ORDER BY Id OFFSET @Page * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<CampaignUser>(sql, new { CampaignId=model.CampaignId, IsUsed=model.IsUsed, Page=model.Page, PageSize = model.PageSize });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllByUserIdAsync
        public async Task<List<CampaignUser>> GetAllByUserIdAsync(string userId, bool isUsed)
        {
            var sql = "SELECT * FROM CampaignUsers WHERE UserId=@UserId AND IsUsed=@IsUsed";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<CampaignUser>(sql, new { UserId=userId, IsUsed = isUsed });
                return result.ToList();
            }
        }
        #endregion
        #region GetByCodeAsync
        public async Task<CampaignUser> GetByCodeAsync(CampaignUserCodeDto model)
        {
            var sql = "SELECT * FROM CampaignUsers WHERE Code=@Code AND IsUsed=@IsUsed";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<CampaignUser>(sql, new { Code=model.Code, IsUsed = model.IsUsed });
                return result;
            }
        }
        #endregion
    }
}
