using Core.Entities;

namespace EC.Services.CategoryAPI.Dtos.CategoryDtos
{
    public class CategoryGetByNamePagingDto:IDto
    {
        public string Name { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}
