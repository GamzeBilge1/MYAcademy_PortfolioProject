namespace MYAcademy_PortfolioProject.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName{ get; set; }
        public string ProjectDescription { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
