using Core.Utilities.Results;
using EC.Services.CategoryAPI.Dtos.CategoryDtos;
using EC.Services.CategoryAPI.Entities;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.CategoryAPI.Services.Abstract
{
    public interface ICategoryService
    {
        Task<DataResult<List<CategoryDto>>> GetAllAsync();
        Task<DataResult<List<CategoryDto>>> GetAllPagingAsync(int page = 1, int pageSize = 8);
        Task<DataResult<List<CategoryDto>>> GetAllSubCategoriesByIdAsync(int id);
        Task<DataResult<List<CategoryDto>>> GetAllSubCategoriesByIdPagingAsync(int id, int page = 1, int pageSize = 8);
        Task<DataResult<List<CategoryDto>>> GetByNameAsync(string name);
        Task<DataResult<List<CategoryDto>>> GetByNamePagingAsync(string name, int page = 1, int pageSize = 8);
        Task<DataResult<CategoryDto>> GetByIdAsync(int id);
        Task<DataResult<CategoryDto>> GetByLinkAsync(string link);
        Task<IResult> AddAsync(CategoryAddDto categoryModel);
        Task<IResult> AddingAsync(CategoryAddDto categoryModel, Category category);
        Task<IResult> UpdateAsync(CategoryUpdateDto model);
        Task<IResult> UpdatingAsync(CategoryUpdateDto categoryModel, Category category);
        Task<IResult> DeleteAsync(CategoryDeleteDto model);
        Task<IResult> DeletingAsync(Category category);
    }
}
