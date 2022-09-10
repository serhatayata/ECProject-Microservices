using Core.Entities;

namespace EC.Services.ProductAPI.Dtos.BaseDtos
{
    public class PagingDto:IDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}
