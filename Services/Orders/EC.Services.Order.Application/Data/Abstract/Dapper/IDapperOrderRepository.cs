using Core.DataAccess.Dapper;

namespace EC.Services.Order.Application.Data.Abstract.Dapper
{
    public interface IDapperOrderRepository : IGenericRepository<Domain.OrderAggregate.Order>
    {
        Task<Domain.OrderAggregate.Order> GetByOrderNoAsync(string orderNo);
        Task<Domain.OrderAggregate.Order> GetByOrderNoAsync(string orderNo,string userId);
        Task<List<Domain.OrderAggregate.Order>> GetAllByUserIdAsync(string userId);
        Task<List<Domain.OrderAggregate.Order>> GetAllPagingAsync(int page = 1, int pageSize = 8);
        Task<List<Domain.OrderAggregate.Order>> GetAllPagingByUserIdAsync(string userId,int page = 1, int pageSize = 8);



    }
}
