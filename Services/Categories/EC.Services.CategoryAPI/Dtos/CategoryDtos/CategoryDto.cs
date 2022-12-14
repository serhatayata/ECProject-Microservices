using Core.Entities;

namespace EC.Services.CategoryAPI.Dtos.CategoryDtos
{
    public class CategoryDto:IDto
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int Line { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
