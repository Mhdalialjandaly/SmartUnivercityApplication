
namespace UniversityManagementSystem.Application.DTOs
{
    public class AssetCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal DefaultDepreciationRate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
