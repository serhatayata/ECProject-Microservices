using Core.Dtos;
using Core.Utilities.Attributes;
using EC.Services.CategoryAPI.Dtos.CategoryDtos;
using EC.Services.CategoryAPI.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.CategoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #region AddAsync
        [HttpPost]
        [Route("add")]
        [AuthorizeAnyPolicy("WriteCategory,FullCategory")]
        public async Task<IActionResult> AddAsync(CategoryAddDto model)
        {
            var result = await _categoryService.AddAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region UpdateAsync
        [HttpPut]
        [Route("update")]
        [AuthorizeAnyPolicy("WriteCategory,FullCategory")]
        public async Task<IActionResult> UpdateAsync(CategoryUpdateDto model)
        {
            var result = await _categoryService.UpdateAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteAsync
        [HttpDelete]
        [Route("delete")]
        [AuthorizeAnyPolicy("WriteCategory,FullCategory")]
        public async Task<IActionResult> DeleteAsync(CategoryDeleteDto model)
        {
            var result = await _categoryService.DeleteAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAsync
        [HttpGet]
        [Route("get")]
        [AuthorizeAnyPolicy("ReadCategory,FullCategory")]
        public async Task<IActionResult> GetAsync([FromQuery] int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        [HttpGet]
        [Route("getall")]
        [AuthorizeAnyPolicy("ReadCategory,FullCategory")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _categoryService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllPagingAsync
        [HttpGet]
        [Route("getall-paging")]
        [AuthorizeAnyPolicy("ReadCategory,FullCategory")]
        public async Task<IActionResult> GetAllPagingAsync([FromQuery] PagingDto model)
        {
            var result = await _categoryService.GetAllPagingAsync(model.Page,model.PageSize);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllSubCategoriesByIdAsync
        [HttpGet]
        [Route("getall-subcategories-byid")]
        [AuthorizeAnyPolicy("ReadCategory,FullCategory")]
        public async Task<IActionResult> GetAllSubCategoriesByIdAsync([FromQuery]int id)
        {
            var result = await _categoryService.GetAllSubCategoriesByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllSubCategoriesByIdPagingAsync
        [HttpGet]
        [Route("getall-subcategories-byid-paging")]
        [AuthorizeAnyPolicy("ReadCategory,FullCategory")]
        public async Task<IActionResult> GetAllSubCategoriesByIdPagingAsync([FromQuery]CategoryGetAllSubCategoriesByIdPagingDto model)
        {
            var result = await _categoryService.GetAllSubCategoriesByIdPagingAsync(model.Id,model.Page,model.PageSize);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetByLinkAsync
        [HttpGet]
        [Route("get-bylink")]
        [AuthorizeAnyPolicy("ReadCategory,FullCategory")]
        public async Task<IActionResult> GetByLinkAsync([FromQuery]string link)
        {
            var result = await _categoryService.GetByLinkAsync(link);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetByNameAsync
        [HttpGet]
        [Route("get-byname")]
        [AuthorizeAnyPolicy("ReadCategory,FullCategory")]
        public async Task<IActionResult> GetByNameAsync([FromQuery] string name)
        {
            var result = await _categoryService.GetByNameAsync(name);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetByNamePagingAsync
        [HttpGet]
        [Route("get-byname-paging")]
        [AuthorizeAnyPolicy("ReadCategory,FullCategory")]
        public async Task<IActionResult> GetByNamePagingAsync([FromQuery]CategoryGetByNamePagingDto model)
        {
            var result = await _categoryService.GetByNamePagingAsync(model.Name,model.Page,model.PageSize);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
    }
}
