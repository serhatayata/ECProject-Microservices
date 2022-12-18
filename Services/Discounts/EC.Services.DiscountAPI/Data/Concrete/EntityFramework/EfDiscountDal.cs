using Core.DataAccess.EntityFramework;
using EC.Services.DiscountAPI.Data.Abstract.EntityFramework;
using EC.Services.DiscountAPI.Data.Contexts;
using EC.Services.DiscountAPI.Entities;

namespace EC.Services.DiscountAPI.Data.Concrete.EntityFramework
{
    public class EfDiscountDal : EfEntityRepositoryBase<Discount, DiscountDbContext>, IDiscountDal
    {

    }
}
