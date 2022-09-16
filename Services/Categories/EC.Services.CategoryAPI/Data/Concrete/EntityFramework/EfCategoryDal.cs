using Core.DataAccess.EntityFramework;
using EC.Services.CategoryAPI.Data.Abstract.EntityFramework;
using EC.Services.CategoryAPI.Data.Contexts;
using EC.Services.CategoryAPI.Entities;
using System;

namespace EC.Services.CategoryAPI.Data.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, CategoryDbContext>, ICategoryDal
    {

    }
}
