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
                      "ot.Id, ot.OrderId, ot.Price, ot.ProductId, ot.Quantity " +
                      "FROM Orders o LEFT JOIN OrderItems ot " +
                      "ON o.Id = ot.OrderId";


            var dict = new Dictionary<int,Domain.OrderAggregate.Order>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order, OrderItem, Domain.OrderAggregate.Order>(sql,
                    (ord, item) =>
                    {
                        Domain.OrderAggregate.Order order;
                        if (!dict.TryGetValue(ord.Id,out order))
                        {
                            order = ord;
                            order.OrderItems = new List<OrderItem>();
                            dict.Add(ord.Id, order);
                        }
                        if (item.OrderId > 0)
                        {
                            order.OrderItems.Add(item);
                        }
                        return order;
                    },splitOn: "Id");
                return result.Distinct().ToList();
            }
        }
        #endregion
        #region GetAllByUserIdAsync
        public async Task<List<Domain.OrderAggregate.Order>> GetAllByUserIdAsync(string userId)
        {
            var sql = "SELECT o.Id, o.UserId, o.CDate, o.OrderNo, o.CountryName, o.CountyName, o.CityName, o.AddressDetail, o.ZipCode, " +
                      "ot.Id, ot.OrderId, ot.Price, ot.ProductId, ot.Quantity " +
                      "FROM Orders o " +
                      "LEFT JOIN OrderItems ot " +
                      "ON o.Id = ot.OrderId " +
                      "WHERE o.UserId=@UserId";

            var dict = new Dictionary<int, Domain.OrderAggregate.Order>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order,OrderItem,Domain.OrderAggregate.Order>(sql, 
                    (ord, item) => 
                                   {
                                       Domain.OrderAggregate.Order order;
                                       if (!dict.TryGetValue(ord.Id, out order))
                                       {
                                           order = ord;
                                           order.OrderItems = new List<OrderItem>();
                                           dict.Add(ord.Id, order);
                                       }
                                       if (item.OrderId > 0)
                                       {
                                           order.OrderItems.Add(item);
                                       }
                                       return order;
                                   }, 
                    new { UserId=userId }, splitOn: "Id");
                return result.Distinct().ToList();
            }
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<List<Domain.OrderAggregate.Order>> GetAllPagingAsync(int page = 1, int pageSize = 8)
        {
            var sql = "SELECT o.Id, o.UserId, o.CDate, o.OrderNo, o.CountryName, o.CountyName, o.CityName, o.AddressDetail, o.ZipCode, " +
                      "ot.Id, ot.OrderId, ot.Price, ot.ProductId, ot.Quantity " +
                      "FROM Orders o LEFT JOIN OrderItems ot " +
                      "ON o.Id = ot.OrderId " +
                      "ORDER BY o.Id OFFSET @Page * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";

            var dict = new Dictionary<int, Domain.OrderAggregate.Order>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order, OrderItem, Domain.OrderAggregate.Order>(sql,
                    (ord, item) =>
                    {
                        Domain.OrderAggregate.Order order;
                        if (!dict.TryGetValue(ord.Id, out order))
                        {
                            order = ord;
                            order.OrderItems = new List<OrderItem>();
                            dict.Add(ord.Id, order);
                        }
                        if (item.OrderId > 0)
                        {
                            order.OrderItems.Add(item);
                        }
                        return order;
                    }, new { Page = page - 1, PageSize = pageSize }, splitOn: "Id");
                return result.Distinct().ToList();
            }
        }
        #endregion
        #region GetAllPagingByUserIdAsync
        public async Task<List<Domain.OrderAggregate.Order>> GetAllPagingByUserIdAsync(string userId, int page = 1, int pageSize = 8)
        {
            var sql = "SELECT o.Id, o.UserId, o.CDate, o.OrderNo, o.CountryName, o.CountyName, o.CityName, o.AddressDetail, o.ZipCode, " +
                      "ot.Id, ot.OrderId, ot.Price, ot.ProductId, ot.Quantity " +
                      "FROM Orders o LEFT JOIN OrderItems ot " +
                      "ON o.Id = ot.OrderId " +
                      "WHERE o.UserId=@UserId " +
                      "ORDER BY o.Id OFFSET @Page * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";

            var dict = new Dictionary<int, Domain.OrderAggregate.Order>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order, OrderItem, Domain.OrderAggregate.Order>(sql,
                    (ord, item) =>
                    {
                        Domain.OrderAggregate.Order order;
                        if (!dict.TryGetValue(ord.Id, out order))
                        {
                            order = ord;
                            order.OrderItems = new List<OrderItem>();
                            dict.Add(ord.Id, order);
                        }
                        if (item.OrderId > 0)
                        {
                            order.OrderItems.Add(item);
                        }
                        return order;
                    }, new { UserId = userId, Page = page - 1, PageSize = pageSize }, splitOn: "Id");
                return result.Distinct().ToList();
            }
        }
        #endregion
        #region GetByIdAsync
        public async Task<Domain.OrderAggregate.Order> GetByIdAsync(int id)
        {
            var sql = "SELECT o.Id, o.UserId, o.CDate, o.OrderNo, o.CountryName, o.CountyName, o.CityName, o.AddressDetail, o.ZipCode, " +
                      "ot.Id, ot.OrderId, ot.Price, ot.ProductId, ot.Quantity " +
                      "FROM Orders o LEFT JOIN OrderItems ot " +
                      "ON o.Id = ot.OrderId " +
                      "WHERE o.Id=@Id";

            var dict = new Dictionary<int, Domain.OrderAggregate.Order>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order, OrderItem, Domain.OrderAggregate.Order>(sql,
                    (ord, item) =>
                    {
                        Domain.OrderAggregate.Order order;
                        if (!dict.TryGetValue(ord.Id, out order))
                        {
                            order = ord;
                            order.OrderItems = new List<OrderItem>();
                            dict.Add(ord.Id, order);
                        }
                        if (item.OrderId > 0)
                        {
                            order.OrderItems.Add(item);
                        }
                        return order;
                    }, new { Id = id }, splitOn: "Id");
                return result?.FirstOrDefault();
            }
        }
        #endregion
        #region GetByOrderNoAsync
        public async Task<Domain.OrderAggregate.Order> GetByOrderNoAsync(string orderNo)
        {
            var sql = "SELECT o.Id, o.UserId, o.CDate, o.OrderNo, o.CountryName, o.CountyName, o.CityName, o.AddressDetail, o.ZipCode, " +
                      "ot.Id, ot.OrderId, ot.Price, ot.ProductId, ot.Quantity " +
                      "FROM Orders o LEFT JOIN OrderItems ot " +
                      "ON o.Id = ot.OrderId " +
                      "WHERE o.OrderNo=@OrderNo";

            var dict = new Dictionary<int, Domain.OrderAggregate.Order>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order, OrderItem, Domain.OrderAggregate.Order>(sql,
                    (ord, item) =>
                    {
                        Domain.OrderAggregate.Order order;
                        if (!dict.TryGetValue(ord.Id, out order))
                        {
                            order = ord;
                            order.OrderItems = new List<OrderItem>();
                            dict.Add(ord.Id, order);
                        }
                        if (item.OrderId > 0)
                        {
                            order.OrderItems.Add(item);
                        }
                        return order;
                    }, new { OrderNo = orderNo }, splitOn: "Id");
                return result?.FirstOrDefault();
            }
        }

        public async Task<Domain.OrderAggregate.Order> GetByOrderNoAsync(string orderNo, string userId)
        {
            var sql = "SELECT o.Id, o.UserId, o.CDate, o.OrderNo, o.CountryName, o.CountyName, o.CityName, o.AddressDetail, o.ZipCode, " +
                      "ot.Id, ot.OrderId, ot.Price, ot.ProductId, ot.Quantity " +
                      "FROM Orders o LEFT JOIN OrderItems ot " +
                      "ON o.Id = ot.OrderId " +
                      "WHERE o.OrderNo=@OrderNo AND UserId=@UserId";

            var dict = new Dictionary<int, Domain.OrderAggregate.Order>();

            using (var connection = new SqlConnection(_defaultConnection))
            {
                connection.Open();
                var result = await connection.QueryAsync<Domain.OrderAggregate.Order, OrderItem, Domain.OrderAggregate.Order>(sql,
                    (ord, item) =>
                    {
                        Domain.OrderAggregate.Order order;
                        if (!dict.TryGetValue(ord.Id, out order))
                        {
                            order = ord;
                            order.OrderItems = new List<OrderItem>();
                            dict.Add(ord.Id, order);
                        }
                        if (item.OrderId > 0)
                        {
                            order.OrderItems.Add(item);
                        }
                        return order;
                    }, new { OrderNo = orderNo, UserId=userId }, splitOn: "Id");
                return result?.FirstOrDefault();
            }
        }
        #endregion

    }
}
