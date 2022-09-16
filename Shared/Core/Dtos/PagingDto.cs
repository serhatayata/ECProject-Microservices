using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class PagingDto:IDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}
