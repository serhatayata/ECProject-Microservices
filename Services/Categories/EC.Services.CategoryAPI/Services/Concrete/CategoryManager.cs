using AutoMapper;
using Core.Utilities.Results;
using EC.Services.CategoryAPI.Data.Abstract.Dapper;
using EC.Services.CategoryAPI.Data.Abstract.EntityFramework;
using EC.Services.CategoryAPI.Dtos.CategoryDtos;
using EC.Services.CategoryAPI.Entities;
using EC.Services.CategoryAPI.Services.Abstract;
using IResult = Core.Utilities.Results.IResult;

namespace EC.Services.CategoryAPI.Services.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryDal categoryDal, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        #region AddAsync
        public async Task<IResult> AddAsync(CategoryAddDto categoryModel)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region AddingAsync
        public async Task<IResult> AddingAsync(CategoryAddDto categoryModel, Category category)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region UpdateAsync
        public async Task<IResult> UpdateAsync(CategoryUpdateDto model)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region UpdatingAsync
        public async Task<IResult> UpdatingAsync(CategoryUpdateDto categoryModel, Category category)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region DeleteAsync
        public async Task<IResult> DeleteAsync(CategoryDeleteDto model)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region DeletingAsync
        public async Task<IResult> DeletingAsync(Category category)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllAsync
        public async Task<DataResult<List<CategoryDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllPagingAsync
        public async Task<DataResult<List<CategoryDto>>> GetAllPagingAsync(int page = 1, int pageSize = 8)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllSubCategoriesByIdAsync
        public async Task<DataResult<List<CategoryDto>>> GetAllSubCategoriesByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetAllSubCategoriesByIdPagingAsync
        public async Task<DataResult<List<CategoryDto>>> GetAllSubCategoriesByIdPagingAsync(int id, int page = 1, int pageSize = 8)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetByIdAsync
        public async Task<DataResult<CategoryDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetByLinkAsync
        public Task<DataResult<CategoryDto>> GetByLinkAsync(string link)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetByNameAsync
        public async Task<DataResult<List<CategoryDto>>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region GetByNamePagingAsync
        public async Task<DataResult<List<CategoryDto>>> GetByNamePagingAsync(string name, int page = 1, int pageSize = 8)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
