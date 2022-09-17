using Core.Entities;

namespace EC.Services.CategoryAPI.Dtos.CategoryDtos
{
    public class CategoryGetAllSubCategoriesByIdPagingDto:IDto
    {
        public int Id { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}
