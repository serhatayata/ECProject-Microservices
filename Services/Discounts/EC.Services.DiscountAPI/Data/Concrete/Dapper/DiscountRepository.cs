using Core.Entities;
using Dapper;
using EC.Services.DiscountAPI.Data.Abstract.Dapper;
using EC.Services.DiscountAPI.Data.Contexts;
using EC.Services.DiscountAPI.Entities;
using Microsoft.Data.SqlClient;
using Nest;

namespace EC.Services.DiscountAPI.Data.Concrete.Dapper
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DiscountDbDapperContext _context;

        public DiscountRepository(DiscountDbDapperContext context)
        {
            _context = context;
        }

        #region AnyAsync
        public async Task<bool> AnyAsync()
        {
            var sql = "SELECT TOP 1 * FROM Discounts WHERE Status=@Status";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Discount>(sql, new { Status = (byte)DiscountStatus.Active });
                if (result.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
        #region GetByIdAsync
        public async Task<Discount> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Discounts WHERE Id=@Id AND Status=@Status";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Discount>(sql, new { Id = id ,Status = (byte)DiscountStatus.Active });
                return result;
            }
        }
        #endregion
        #region GetAllAsync
        public async Task<List<Discount>> GetAllAsync()
        {
            var sql = "SELECT * FROM Discounts WHERE Status=@Status";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Discount>(sql, new { Status = (byte)DiscountStatus.Active });
                return result.ToList();
            }
        }
        #endregion
        #region GetWithStatusByIdAsync
        public async Task<Discount> GetWithStatusByIdAsync(int id, DiscountStatus status = DiscountStatus.Active)
        {
            var sql = "SELECT * FROM Discounts WHERE Id=@Id AND Status=@Status";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Discount>(sql, new { Id = id, Status = status });
                return result;
            }
        }
        #endregion
        #region GetAllWithStatusAsync
        public async Task<List<Discount>> GetAllWithStatusAsync(DiscountStatus status = DiscountStatus.Active)
        {
            var sql = "SELECT * FROM Discounts WHERE Status=@Status";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Discount>(sql, new { Status = status });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<List<Discount>> GetAllPagingAsync(int page = 1, int pageSize = 8, DiscountStatus status = DiscountStatus.Active)
        {
            var sql = "SELECT * FROM Discounts WHERE Status=@Status ORDER BY Id OFFSET @Page * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Discount>(sql, new { Page = page - 1, PageSize = pageSize, Status = status });
                return result.ToList();
            }
        }
        #endregion
        #region GetByCodeAsync
        public async Task<Discount> GetByCodeAsync(string code, DiscountStatus status = DiscountStatus.Active)
        {
            var sql = "SELECT * FROM Discounts WHERE Code=@Code AND Status=@Status";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Discount>(sql, new { Code = code , Status = status });
                return result;
            }
        }
        #endregion
        #region GetAllByDiscountTypeAsync
        public async Task<List<Discount>> GetAllByDiscountTypeAsync(int discountType, DiscountStatus status = DiscountStatus.Active)
        {
            var sql = "SELECT * FROM Discounts WHERE DiscountType=@DiscountType AND Status=@Status";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Discount>(sql, new { DiscountType = discountType , Status = status });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllBySponsorAsync
        public async Task<List<Discount>> GetAllBySponsorAsync(string sponsor, DiscountStatus status = DiscountStatus.Active)
        {
            var sql = "SELECT * FROM Discounts WHERE Sponsor=@Sponsor AND Status=@Status";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Discount>(sql, new { Sponsor = sponsor , Status = status });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllBetweenDatesByCreatedTimeAsync
        public async Task<List<Discount>> GetAllBetweenDatesByCreatedTimeAsync(DateTime bTime, DateTime eTime, DiscountStatus status = DiscountStatus.Active)
        {
            var sql = "SELECT * FROM Discounts WHERE Status=@Status AND CDate BETWEEN @BeginningTime and @EndingTime";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Discount>(sql, new { BeginningTime = bTime, EndingTime = eTime, Status = status });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllBetweenDatesByExpirationTimeAsync
        public async Task<List<Discount>> GetAllBetweenDatesByExpirationTimeAsync(DateTime bTime, DateTime eTime, DiscountStatus status = DiscountStatus.Active)
        {
            var sql = "SELECT * FROM Discounts WHERE Status=@Status AND ExpirationDate BETWEEN @BeginningTime and @EndingTime";
            using (var connection = _context.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<Discount>(sql, new { BeginningTime = bTime, EndingTime = eTime, Status = status });
                return result.ToList();
            }
        }
        #endregion
    }
}
