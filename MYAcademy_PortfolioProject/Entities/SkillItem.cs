
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MYAcademy_PortfolioProject.Entities
{
    public class SkillItem
    {
        public int SkillItemId { get; set; }

        [Required]
        [StringLength(100)]
        public string SkillItemTitle { get; set; }

        [Range(0, 100)]
        public int Level { get; set; }

        public int SkillId { get; set; }

        [ValidateNever]
        public Skill Skill { get; set; }
    }
}
