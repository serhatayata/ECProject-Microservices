using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{
    public class HttpContextHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public static HttpContext Current
        {
            get
            {
                return _httpContextAccessor.HttpContext;
            }
        }

        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;

    }
}
