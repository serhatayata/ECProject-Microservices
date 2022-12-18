using Core.DataAccess.Dapper;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Data.Abstract.Dapper
{
    public interface IDiscountRepository : IGenericRepository<Discount>
    {
        Task<Discount> GetByCodeAsync(string code);
        Task<List<Discount>> GetAllPagingAsync(int page = 1, int pageSize = 8);
        Task<List<Discount>> GetAllByDiscountTypeAsync(int discountType);
        Task<List<Discount>> GetAllBySponsorAsync(string sponsor);
        Task<List<Discount>> GetAllBetweenDatesByExpirationTimeAsync(DateTime bTime, DateTime eTime);
        Task<List<Discount>> GetAllBetweenDatesByCreatedTimeAsync(DateTime bTime, DateTime eTime);
    }
}
