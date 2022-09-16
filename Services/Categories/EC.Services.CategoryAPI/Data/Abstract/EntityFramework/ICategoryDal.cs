using Core.DataAccess;
using EC.Services.CategoryAPI.Entities;

namespace EC.Services.CategoryAPI.Data.Abstract.EntityFramework
{
    public interface ICategoryDal : IEntityRepository<Category>
    {

    }
}
