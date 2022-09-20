using Core.DataAccess.EntityFramework;
using EC.Services.PhotoStockAPI.Data.Abstract.EntityFramework;
using EC.Services.PhotoStockAPI.Data.Contexts;
using EC.Services.PhotoStockAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EC.Services.PhotoStockAPI.Data.Concrete.EntityFramework
{
    public class EfPhotoRepository : EfEntityRepositoryBase<Photo, PhotoStockDbContext>, IEfPhotoRepository
    {

    }
}
