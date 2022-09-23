using Core.Utilities.Attributes;
using EC.Services.PhotoStockAPI.Dtos;
using EC.Services.PhotoStockAPI.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.PhotoStockAPI.Controllers
{
    [Route("photostock/api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        #region AddAsync
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromForm]PhotoAddDto model)
        {
            var result = await _photoService.AddAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteByPhotoTypeAndEntityIdAsync
        [HttpDelete]
        [Route("delete-by-type-and-entityid")]
        public async Task<IActionResult> DeleteByPhotoTypeAndEntityIdAsync(PhotoDeleteByTypeAndEntityIdDto model)
        {
            var result = await _photoService.DeleteAllByTypeAndEntityIdAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteByPhotoTypeAsync
        [HttpDelete]
        [Route("delete-by-id")]
        public async Task<IActionResult> DeleteByIdAsync(PhotoDeleteByIdDto model)
        {
            var result = await _photoService.DeleteByIdAsync(model.Id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region DeleteByPhotoTypeAsync
        [HttpDelete]
        [Route("delete-by-url")]
        public async Task<IActionResult> DeleteByUrlAsync(PhotoDeleteByUrlDto model)
        {
            var result = await _photoService.DeleteByUrlAsync(model.Url);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetByIdAsync
        [HttpGet]
        [Route("get-by-id")]
        public async Task<IActionResult> GetByIdAsync([FromQuery]int id)
        {
            var result = await _photoService.GetByIdAsync(id);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllAsync
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _photoService.GetAllAsync();
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllByTypeAndEntityIdAsync
        [HttpGet]
        [Route("get-all-by-type-and-entity-id")]
        public async Task<IActionResult> GetAllByTypeAndEntityIdAsync([FromQuery]PhotoGetAllByTypeAndEntityIdDto model)
        {
            var result = await _photoService.GetAllByTypeAndEntityIdAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion
        #region GetAllByTypeAsync
        [HttpGet]
        [Route("get-all-by-type")]
        public async Task<IActionResult> GetAllByTypeAsync([FromQuery] PhotoGetAllByTypeDto model)
        {
            var result = await _photoService.GetAllByTypeAsync(model);
            return StatusCode(result.StatusCode, result);
        }
        #endregion


    }
}
