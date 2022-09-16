using EC.Services.CategoryAPI.Data.Abstract.Dapper;
using EC.Services.CategoryAPI.Entities;

namespace EC.Services.CategoryAPI.Data.Concrete.Dapper
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task<bool> AnyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyByLinkAsync(string link)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByLinkAsync(string link)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> GetByNamePagingAsync(string name, int page = 1, int pageSize = 8)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetLastLineAsync(int upperCategoryId)
        {
            throw new NotImplementedException();
        }
    }
}
