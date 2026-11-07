using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class About
    {
        public int AboutId { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description1 { get; set; }

        public string Description2 { get; set; }

        public string Statistic1Value { get; set; }
        public string Statistic1Title { get; set; }

        public string Statistic2Value { get; set; }
        public string Statistic2Title { get; set; }

        public string Statistic3Value { get; set; }
        public string Statistic3Title { get; set; }

        public string CvUrl { get; set; }
    }
}