using Dapper;
using EC.Services.PaymentAPI.Data.Abstract.Dapper;
using EC.Services.PaymentAPI.Entities;
using Microsoft.Data.SqlClient;
using Nest;
using System.Drawing.Printing;

namespace EC.Services.PaymentAPI.Data.Concrete.Dapper
{
    public class DapperPaymentRepository : IDapperPaymentRepository
    {
        private readonly IConfiguration _configuration;

        public DapperPaymentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region AnyAsync
        public async Task<bool> AnyAsync()
        {
            var sql = "SELECT TOP 1 * FROM Payments";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Payment>(sql, new { Node = 0 });
                if (result.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
        #region GetAllAsync
        public async Task<List<Payment>> GetAllAsync()
        {
            var sql = "SELECT * FROM Payments";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Payment>(sql);
                return result.ToList();
            }
        }
        #endregion
        #region GetByIdAsync
        public async Task<Payment> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Payments WHERE Id=@Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Payment>(sql, new { Id = id });
                return result;
            }
        }
        #endregion
        #region GetByPaymentNoAsync
        public async Task<Payment> GetByPaymentNoAsync(string paymentNo)
        {
            var sql = "SELECT * FROM Payments WHERE PaymentNo=@PaymentNo";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Payment>(sql, new { PaymentNo = paymentNo });
                return result;
            }
        }
        #endregion
        #region GetAllByUserIdAsync
        public async Task<List<Payment>> GetAllByUserIdAsync(string userId)
        {
            var sql = "SELECT * FROM Payments WHERE UserId=@UserId";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Payment>(sql, new {UserId=userId});
                return result.ToList();
            }
        }
        #endregion
        #region GetAllByUserIdBetweenDatesAsync
        public async Task<List<Payment>> GetAllByUserIdBetweenDatesAsync(string userId, DateTime bTime, DateTime eTime)
        {
            var sql = "SELECT * FROM Payments WHERE UserId=@UserId AND CDate between @BeginningTime and @EndingTime";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Payment>(sql, new { UserId=userId, BeginningTime=bTime, EndingTime=eTime });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllByUserIdPagingAsync
        public async Task<List<Payment>> GetAllByUserIdPagingAsync(string userId, int page = 1, int pageSize = 8)
        {
            var sql = "SELECT * FROM Payments WHERE UserId=@UserId ORDER BY Id OFFSET @Page * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Payment>(sql, new { UserId=userId, Page = page - 1, PageSize = pageSize });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<List<Payment>> GetAllPagingAsync(int page = 1, int pageSize = 8)
        {
            var sql = "SELECT * FROM Payments ORDER BY Id OFFSET @Page * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Payment>(sql, new { Page = page - 1, PageSize = pageSize });
                return result.ToList();
            }
        }
        #endregion

    }
}
