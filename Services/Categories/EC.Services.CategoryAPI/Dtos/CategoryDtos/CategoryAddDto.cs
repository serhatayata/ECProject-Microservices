using Core.Entities;

namespace EC.Services.CategoryAPI.Dtos.CategoryDtos
{
    public class CategoryAddDto:IDto
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}
