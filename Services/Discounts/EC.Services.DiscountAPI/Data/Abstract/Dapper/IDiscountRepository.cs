using Core.DataAccess;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Data.Abstract.Dapper
{
    public interface IDiscountRepository : IGenericRepository<Discount>
    {
        Task<Discount> GetByCodeAsync(string code, DiscountStatus status = DiscountStatus.Active);
        Task<Discount> GetWithStatusByIdAsync(int id, DiscountStatus status = DiscountStatus.Active);
        Task<List<Discount>> GetAllWithStatusAsync(DiscountStatus status = DiscountStatus.Active);
        Task<List<Discount>> GetAllPagingAsync(int page = 1, int pageSize = 8, DiscountStatus status = DiscountStatus.Active);
        Task<List<Discount>> GetAllByDiscountTypeAsync(int discountType, DiscountStatus status = DiscountStatus.Active);
        Task<List<Discount>> GetAllBySponsorAsync(string sponsor, DiscountStatus status = DiscountStatus.Active);
        Task<List<Discount>> GetAllBetweenDatesByExpirationTimeAsync(DateTime bTime, DateTime eTime, DiscountStatus status = DiscountStatus.Active);
        Task<List<Discount>> GetAllBetweenDatesByCreatedTimeAsync(DateTime bTime, DateTime eTime, DiscountStatus status = DiscountStatus.Active);
    }
}
