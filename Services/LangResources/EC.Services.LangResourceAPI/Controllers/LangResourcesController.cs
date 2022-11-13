using EC.Services.LangResourceAPI.Dtos.LangResourceDtos;
using EC.Services.LangResourceAPI.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.Services.LangResourceAPI.Controllers
{
    [Route("langresource/api/[controller]")]
    [ApiController]
    public class LangResourcesController : ControllerBase
    {
        private readonly ILangResourceService _langResourceService;

        public LangResourcesController(ILangResourceService langResourceService)
        {
            _langResourceService = langResourceService;
        }

        #region AddAsync
        public async Task<IActionResult> AddAsync(LangResourceAddDto model)
        {


        }
        #endregion



    }
}
