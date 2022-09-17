using AutoMapper;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Extensions;
using Core.Utilities.Results;
using EC.Services.CategoryAPI.Constants;
using EC.Services.CategoryAPI.Data.Abstract.Dapper;
using EC.Services.CategoryAPI.Data.Abstract.EntityFramework;
using EC.Services.CategoryAPI.Dtos.CategoryDtos;
using EC.Services.CategoryAPI.Entities;
using EC.Services.CategoryAPI.Services.Abstract;
using Microsoft.Extensions.Caching.Memory;
using Nest;
using System.ComponentModel.Design;
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
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        [RedisCacheRemoveAspect("ICategoryService", Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> AddAsync(CategoryAddDto categoryModel)
        {
            var newCategory = _mapper.Map<Category>(categoryModel);

            var resultAdd = await this.AddingAsync(categoryModel, newCategory);
            if (resultAdd.Success)
            {
                return new SuccessResult(resultAdd.Message, resultAdd.StatusCode);
            }
            return new ErrorResult(resultAdd.Message, resultAdd.StatusCode);
        }
        #endregion
        #region AddingAsync
        public async Task<IResult> AddingAsync(CategoryAddDto categoryModel, Category category)
        {
            #region Link
            Guid guidValue = Guid.NewGuid();
            var link = SeoLinkExtensions.GenerateSlug(categoryModel.Name, guidValue.ToString());
            var categoryExists = await _categoryRepository.GetByLinkAsync(link);
            if (categoryExists?.Id != null)
            {
                Guid nextGuid = Guid.NewGuid();
                link = SeoLinkExtensions.GenerateSlug(categoryModel.Name, nextGuid.ToString());
            }
            #endregion
            #region Line
            if (categoryModel.ParentId != null)
            {
                var lastCategoryLine = await _categoryRepository.GetLastLineAsync((int)categoryModel.ParentId);
                if (lastCategoryLine != null)
                {
                    category.Line = lastCategoryLine.Line + 1;
                }
                else
                {
                    category.Line = 1;
                }
            }
            else
            {
                category.Line = 1;
            }
            #endregion

            category.Link = link;
            await _categoryDal.AddAsync(category);
            if (!await _categoryRepository.AnyByLinkAsync(link))
            {
                return new ErrorResult(MessageExtensions.AlreadyExists(CategoryEntities.Category), StatusCodes.Status500InternalServerError);
            }
            return new SuccessResult(MessageExtensions.Added(CategoryEntities.Category), StatusCodes.Status200OK);
        }
        #endregion
        #region UpdateAsync
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        [RedisCacheRemoveAspect("ICategoryService", Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> UpdateAsync(CategoryUpdateDto model)
        {
            var categoryExists = await _categoryRepository.GetByIdAsync(model.Id);
            if (categoryExists == null)
            {
                return new ErrorResult(MessageExtensions.NotUpdated(CategoryEntities.Category), StatusCodes.Status400BadRequest);
            }
            var result = await this.UpdatingAsync(model, categoryExists);
            if (result.Success)
            {
                return new SuccessResult(MessageExtensions.Updated(CategoryEntities.Category), StatusCodes.Status200OK);
            }
            return new ErrorResult(MessageExtensions.NotUpdated(CategoryEntities.Category), StatusCodes.Status500InternalServerError);
        }
        #endregion
        #region UpdatingAsync
        public async Task<IResult> UpdatingAsync(CategoryUpdateDto categoryModel, Category category)
        {
            var categoryUpdated = _mapper.Map<CategoryUpdateDto, Category>(categoryModel, category);
            if (categoryModel.Name != category.Name)
            {
                Guid guidValue = Guid.NewGuid();
                var link = SeoLinkExtensions.GenerateSlug(categoryModel.Name, guidValue.ToString());
                categoryUpdated.Link = link;
            }
            await _categoryDal.UpdateAsync(categoryUpdated);
            return new SuccessResult(MessageExtensions.Updated(CategoryEntities.Category), StatusCodes.Status200OK);
        }
        #endregion
        #region DeleteAsync
        [TransactionScopeAspect(Priority = (int)CacheItemPriority.High)]
        [RedisCacheRemoveAspect("ICategoryService", Priority = (int)CacheItemPriority.High)]
        public async Task<IResult> DeleteAsync(CategoryDeleteDto model)
        {
            var categoryExists = await _categoryRepository.GetByIdAsync(model.Id);
            if (categoryExists == null)
            {
                return new ErrorResult(MessageExtensions.NotUpdated(CategoryEntities.Category), StatusCodes.Status400BadRequest);
            }
            var result = await this.DeletingAsync(categoryExists);
            if (result.Success)
            {
                return new SuccessResult(MessageExtensions.Deleted(CategoryEntities.Category), StatusCodes.Status200OK);
            }
            return new ErrorResult(MessageExtensions.NotDeleted(CategoryEntities.Category), StatusCodes.Status500InternalServerError);
        }
        #endregion
        #region DeletingAsync
        public async Task<IResult> DeletingAsync(Category category)
        {
            await _categoryDal.DeleteAsync(category);
            if (!await _categoryRepository.AnyByLinkAsync(category.Link))
            {
                return new ErrorResult(MessageExtensions.NotDeleted(CategoryEntities.Category), StatusCodes.Status500InternalServerError);
            }
            return new SuccessResult(MessageExtensions.Deleted(CategoryEntities.Category), StatusCodes.Status200OK);
        }
        #endregion
        #region GetAllAsync
        [RedisCacheAspect<DataResult<List<CategoryDto>>>(duration: 60)]
        public async Task<DataResult<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (categories == null)
            {
                return new ErrorDataResult<List<CategoryDto>>(MessageExtensions.NotFound(CategoryEntities.Category));
            }
            else if (categories.Count == 0)
            {
                return new ErrorDataResult<List<CategoryDto>>(MessageExtensions.NotFound(CategoryEntities.Category),StatusCodes.Status200OK);
            }
            var result = _mapper.Map<List<CategoryDto>>(categories);
            return new SuccessDataResult<List<CategoryDto>>(result);
        }
        #endregion
        #region GetAllPagingAsync
        [RedisCacheAspect<DataResult<List<CategoryDto>>>(duration: 60)]
        public async Task<DataResult<List<CategoryDto>>> GetAllPagingAsync(int page = 1, int pageSize = 8)
        {
            var categories = await _categoryRepository.GetAllPagingAsync(page,pageSize);
            if (categories == null)
            {
                return new ErrorDataResult<List<CategoryDto>>(MessageExtensions.NotFound(CategoryEntities.Category));
            }
            else if (categories.Count == 0)
            {
                return new ErrorDataResult<List<CategoryDto>>(MessageExtensions.NotFound(CategoryEntities.Category), StatusCodes.Status200OK);
            }
            var result = _mapper.Map<List<CategoryDto>>(categories);
            return new SuccessDataResult<List<CategoryDto>>(result);
        }
        #endregion
        #region GetAllSubCategoriesByIdAsync
        [RedisCacheAspect<DataResult<List<CategoryDto>>>(duration: 60)]
        public async Task<DataResult<List<CategoryDto>>> GetAllSubCategoriesByIdAsync(int id)
        {
            var categories = await _categoryRepository.GetAllSubCategoriesByIdAsync(id);
            if (categories == null)
            {
                return new ErrorDataResult<List<CategoryDto>>(MessageExtensions.NotFound(CategoryEntities.Category));
            }
            else if (categories.Count == 0)
            {
                return new ErrorDataResult<List<CategoryDto>>(MessageExtensions.NotFound(CategoryEntities.Category), StatusCodes.Status200OK);
            }
            var result = _mapper.Map<List<CategoryDto>>(categories);
            return new SuccessDataResult<List<CategoryDto>>(result);
        }
        #endregion
        #region GetAllSubCategoriesByIdPagingAsync
        [RedisCacheAspect<DataResult<List<CategoryDto>>>(duration: 60)]
        public async Task<DataResult<List<CategoryDto>>> GetAllSubCategoriesByIdPagingAsync(int id, int page = 1, int pageSize = 8)
        {
            var categories = await _categoryRepository.GetAllSubCategoriesByIdPagingAsync(id,page, pageSize);
            if (categories == null)
            {
                return new ErrorDataResult<List<CategoryDto>>(MessageExtensions.NotFound(CategoryEntities.Category));
            }
            else if (categories.Count == 0)
            {
                return new ErrorDataResult<List<CategoryDto>>(MessageExtensions.NotFound(CategoryEntities.Category), StatusCodes.Status200OK);
            }
            var result = _mapper.Map<List<CategoryDto>>(categories);
            return new SuccessDataResult<List<CategoryDto>>(result);
        }
        #endregion
        #region GetByIdAsync
        public async Task<DataResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return new ErrorDataResult<CategoryDto>(MessageExtensions.NotFound(CategoryEntities.Category), StatusCodes.Status200OK);
            }
            var result = _mapper.Map<CategoryDto>(category);
            return new SuccessDataResult<CategoryDto>(result);
        }
        #endregion
        #region GetByLinkAsync
        public async Task<DataResult<CategoryDto>> GetByLinkAsync(string link)
        {
            var category = await _categoryRepository.GetByLinkAsync(link);
            if (category == null)
            {
                return new ErrorDataResult<CategoryDto>(MessageExtensions.NotFound(CategoryEntities.Category), StatusCodes.Status200OK);
            }
            var result = _mapper.Map<CategoryDto>(category);
            return new SuccessDataResult<CategoryDto>(result);
        }
        #endregion
        #region GetByNameAsync
        [RedisCacheAspect<DataResult<List<CategoryDto>>>(duration: 60)]
        public async Task<DataResult<List<CategoryDto>>> GetByNameAsync(string name)
        {
            var categories = await _categoryRepository.GetByNameAsync(name);
            if (categories == null)
            {
                return new ErrorDataResult<List<CategoryDto>>(MessageExtensions.NotFound(CategoryEntities.Category));
            }
            else if (categories.Count == 0)
            {
                return new ErrorDataResult<List<CategoryDto>>(MessageExtensions.NotFound(CategoryEntities.Category), StatusCodes.Status200OK);
            }
            var result = _mapper.Map<List<CategoryDto>>(categories);
            return new SuccessDataResult<List<CategoryDto>>(result);
        }
        #endregion
        #region GetByNamePagingAsync
        [RedisCacheAspect<DataResult<List<CategoryDto>>>(duration: 60)]
        public async Task<DataResult<List<CategoryDto>>> GetByNamePagingAsync(string name, int page = 1, int pageSize = 8)
        {
            var categories = await _categoryRepository.GetByNamePagingAsync(name,page, pageSize);
            if (categories == null)
            {
                return new ErrorDataResult<List<CategoryDto>>(MessageExtensions.NotFound(CategoryEntities.Category));
            }
            else if (categories.Count == 0)
            {
                return new ErrorDataResult<List<CategoryDto>>(MessageExtensions.NotFound(CategoryEntities.Category), StatusCodes.Status200OK);
            }
            var result = _mapper.Map<List<CategoryDto>>(categories);
            return new SuccessDataResult<List<CategoryDto>>(result);
        }
        #endregion

    }
}
