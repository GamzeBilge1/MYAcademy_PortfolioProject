
using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(2000)]
        public string ProjectDescription { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(200)]
        public string Technology { get; set; }

        public bool isActive { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
