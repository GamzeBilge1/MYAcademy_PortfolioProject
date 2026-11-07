using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.ViewModels
{
    public class DashboardViewModel
    {
        // İstatistikler
        public int TotalProjects { get; set; }
        public int ActiveProjects { get; set; }
        public int TotalSkills { get; set; }
        public int TotalExperiences { get; set; }
        public int TotalEducations { get; set; }
        public int TotalMessages { get; set; }
        public int UnreadMessages { get; set; }
        public int ReadMessages { get; set; }
        public List<Message> LastMessages { get; set; }
        public Dictionary<string, int> CategorySummaries { get; set; } 
        public List<Project> LastProjects { get; set; } 
    }
}
