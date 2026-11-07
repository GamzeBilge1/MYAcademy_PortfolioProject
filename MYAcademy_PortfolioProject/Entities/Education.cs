
using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Education
    {
        public int EducationId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(150)]
        public string SchoolName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public bool isActive { get; set; }
    }
}
