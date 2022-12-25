using Core.Dtos;
using Core.Utilities.Results;
using Dapper;
using EC.Services.DiscountAPI.Data.Abstract.Dapper;
using EC.Services.DiscountAPI.Data.Contexts;
using EC.Services.DiscountAPI.Entities;
using Nest;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.DiscountAPI.Data.Concrete.Dapper
{
    public class CampaignProductRepository : ICampaignProductRepository
    {
        private readonly DiscountDbDapperContext _context;

        public CampaignProductRepository(DiscountDbDapperContext context)
        {
            _context = context;
        }


        #region DeleteAllProductsByCampaignIdAsync
        public async Task<IResult> DeleteAllProductsByCampaignIdAsync(DeleteIntDto model)
        {
            var sql = "DELETE [CampaignProducts] WHERE CampaignId=@CampaignId";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(sql, new { CampaignId = model.Id });
            }
            return new SuccessResult();
        }
        #endregion
        #region GetAllByProductIdsAsync
        public async Task<List<CampaignProduct>> GetAllByProductIdsAsync(int[] productIds)
        {
            var sql = "SELECT * FROM CampaignProducts WHERE ProductId IN @productIds";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<CampaignProduct>(sql, new { productIds = productIds });
                return result.ToList();
            }
        }
        #endregion
    }
}
