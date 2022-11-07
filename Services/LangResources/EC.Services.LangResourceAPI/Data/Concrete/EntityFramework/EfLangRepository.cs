using Core.DataAccess.EntityFramework;
using EC.Services.LangResourceAPI.Data.Abstract.EntityFramework;
using EC.Services.LangResourceAPI.Data.Contexts;
using EC.Services.LangResourceAPI.Entities;

namespace EC.Services.LangResourceAPI.Data.Concrete.EntityFramework
{
    public class EfLangRepository : EfEntityRepositoryBase<Lang, LangResourceDbContext>, IEfLangRepository
    {


    }
}
