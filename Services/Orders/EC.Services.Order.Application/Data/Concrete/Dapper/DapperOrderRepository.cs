using Dapper;
using EC.Services.Order.Application.Data.Abstract.Dapper;
using EC.Services.Order.Domain.OrderAggregate;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Nest;

namespace EC.Services.Order.Application.Data.Concrete.Dapper
{
    public class DapperOrderRepository:IDapperOrderRepository
    {
        private readonly IConfiguration _configuration;
        private string _defaultConnection;
        public DapperOrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _defaultConnection = _configuration.GetConnectionString("DefaultConnection");
        }

        #region AnyAsync
        public async Task<bool> AnyAsync()
        {
            var sql = "SELECT TOP 1 * FROM Orders";
            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order>(sql);
                if (result.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
        #region GetAllAsync
        public async Task<List<Domain.OrderAggregate.Order>> GetAllAsync()
        {
            var sql = "SELECT o.Id, o.UserId, o.CDate, o.OrderNo, o.CountryName, o.CountyName, o.CityName, o.AddressDetail, o.ZipCode, " +
                      "ot.Id, ot.OrderId, ot.Price, ot.ProductId " +
                      "FROM Orders o LEFT JOIN OrderItems ot " +
                      "ON o.Id = ot.OrderId";

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order, OrderItem, Domain.OrderAggregate.Order>(sql,
                    (ord, item) =>
                    {
                        ord.OrderItems = new List<OrderItem>();
                        ord.OrderItems.Add(item);
                        return ord;
                    });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllByUserIdAsync
        public async Task<List<Domain.OrderAggregate.Order>> GetAllByUserIdAsync(string userId)
        {
            var sql = "SELECT o.Id, o.UserId, o.CDate, o.OrderNo, o.CountryName, o.CountyName, o.CityName, o.AddressDetail, o.ZipCode, " +
                      "ot.Id, ot.OrderId, ot.Price, ot.ProductId " +
                      "FROM Orders o " +
                      "LEFT JOIN OrderItems ot " +
                      "ON o.Id = ot.OrderId " +
                      "WHERE o.UserId=@UserId";

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order,OrderItem,Domain.OrderAggregate.Order>(sql, 
                    (ord, item) => 
                                   {
                                       ord.OrderItems = new List<OrderItem>();
                                       ord.OrderItems.Add(item);
                                       return ord;
                                   }, 
                    new { UserId=userId });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<List<Domain.OrderAggregate.Order>> GetAllPagingAsync(int page = 1, int pageSize = 8)
        {
            var sql = "SELECT o.Id, o.UserId, o.CDate, o.OrderNo, o.CountryName, o.CountyName, o.CityName, o.AddressDetail, o.ZipCode, " +
                      "ot.Id, ot.OrderId, ot.Price, ot.ProductId " +
                      "FROM Orders o LEFT JOIN OrderItems ot " +
                      "ON o.Id = ot.OrderId " +
                      "ORDER BY o.Id OFFSET @Page * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order, OrderItem, Domain.OrderAggregate.Order>(sql,
                    (ord, item) =>
                    {
                        ord.OrderItems = new List<OrderItem>();
                        ord.OrderItems.Add(item);
                        return ord;
                    }, new { Page = page - 1, PageSize = pageSize });
                return result.ToList();
            }
        }
        #endregion
        #region GetAllPagingByUserIdAsync
        public async Task<List<Domain.OrderAggregate.Order>> GetAllPagingByUserIdAsync(string userId, int page = 1, int pageSize = 8)
        {
            var sql = "SELECT o.Id, o.UserId, o.CDate, o.OrderNo, o.CountryName, o.CountyName, o.CityName, o.AddressDetail, o.ZipCode, " +
                      "ot.Id, ot.OrderId, ot.Price, ot.ProductId " +
                      "FROM Orders o LEFT JOIN OrderItems ot " +
                      "ON o.Id = ot.OrderId " +
                      "WHERE o.UserId=@UserId " +
                      "ORDER BY o.Id OFFSET @Page * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order, OrderItem, Domain.OrderAggregate.Order>(sql,
                    (ord, item) =>
                    {
                        ord.OrderItems = new List<OrderItem>();
                        ord.OrderItems.Add(item);
                        return ord;
                    }, new { UserId = userId, Page = page - 1, PageSize = pageSize });
                return result.ToList();
            }
        }
        #endregion
        #region GetByIdAsync
        public async Task<Domain.OrderAggregate.Order> GetByIdAsync(int id)
        {
            var sql = "SELECT o.Id, o.UserId, o.CDate, o.OrderNo, o.CountryName, o.CountyName, o.CityName, o.AddressDetail, o.ZipCode, " +
                      "ot.Id, ot.OrderId, ot.Price, ot.ProductId " +
                      "FROM Orders o LEFT JOIN OrderItems ot " +
                      "ON o.Id = ot.OrderId " +
                      "WHERE o.Id=@Id";
            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order, OrderItem, Domain.OrderAggregate.Order>(sql,
                    (ord, item) =>
                    {
                        ord.OrderItems = new List<OrderItem>();
                        ord.OrderItems.Add(item);
                        return ord;
                    }, new { Id = id });
                return result?.FirstOrDefault();
            }
        }
        #endregion
        #region GetByOrderNoAsync
        public async Task<Domain.OrderAggregate.Order> GetByOrderNoAsync(string orderNo)
        {
            var sql = "SELECT o.Id, o.UserId, o.CDate, o.OrderNo, o.CountryName, o.CountyName, o.CityName, o.AddressDetail, o.ZipCode, " +
                      "ot.Id, ot.OrderId, ot.Price, ot.ProductId " +
                      "FROM Orders o LEFT JOIN OrderItems ot " +
                      "ON o.Id = ot.OrderId " +
                      "WHERE o.OrderNo=@OrderNo"; 
            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order, OrderItem, Domain.OrderAggregate.Order>(sql,
                    (ord, item) =>
                    {
                        ord.OrderItems = new List<OrderItem>();
                        ord.OrderItems.Add(item);
                        return ord;
                    }, new { OrderNo = orderNo });
                return result?.FirstOrDefault();
            }
        }

        public async Task<Domain.OrderAggregate.Order> GetByOrderNoAsync(string orderNo, string userId)
        {
            var sql = "SELECT o.Id, o.UserId, o.CDate, o.OrderNo, o.CountryName, o.CountyName, o.CityName, o.AddressDetail, o.ZipCode, " +
                      "ot.Id, ot.OrderId, ot.Price, ot.ProductId " +
                      "FROM Orders o LEFT JOIN OrderItems ot " +
                      "ON o.Id = ot.OrderId " +
                      "WHERE o.OrderNo=@OrderNo AND UserId=@UserId"; 
            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order, OrderItem, Domain.OrderAggregate.Order>(sql,
                    (ord, item) =>
                    {
                        ord.OrderItems = new List<OrderItem>();
                        ord.OrderItems.Add(item);
                        return ord;
                    }, new { OrderNo = orderNo, UserId=userId });
                return result?.FirstOrDefault();
            }
        }
        #endregion

    }
}
