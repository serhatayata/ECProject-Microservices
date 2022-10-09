using Core.DataAccess.Dapper;
using EC.Services.PaymentAPI.Entities;

namespace EC.Services.PaymentAPI.Data.Abstract.Dapper
{
    public interface IDapperPaymentRepository : IGenericRepository<Payment>
    {
        Task<List<Payment>> GetAllPagingAsync(int page = 1, int pageSize = 8);
        Task<List<Payment>> GetAllByUserIdAsync(string userId);
        Task<List<Payment>> GetAllByUserIdBetweenDatesAsync(string userId,DateTime bTime, DateTime eTime);
        Task<List<Payment>> GetAllByUserIdPagingAsync(string userId, int page = 1, int pageSize = 8);

    }
}
