﻿using Core.DataAccess.Dapper;
using EC.Services.CategoryAPI.Entities;

namespace EC.Services.CategoryAPI.Data.Abstract.Dapper
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<bool> AnyByLinkAsync(string link);
        Task<Category> GetLastLineAsync(int upperCategoryId);
        Task<List<Category>> GetByNameAsync(string name);
        Task<List<Category>> GetByNamePagingAsync(string name, int page = 1, int pageSize = 8);
        Task<Category> GetByLinkAsync(string link);
    }
}