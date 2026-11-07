using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly PortfolioContext _context;

        public ProjectController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ProjectList(string sort, int? categoryId)
        {
            var values = _context.Projects.Include(x=>x.Category).ToList();
            if (sort == "newest")
            {
                values = values.OrderByDescending(x => x.ProjectId).ToList();
            }
            else
            {
                values = values.OrderBy(x => x.ProjectId).ToList();
            }
            if (categoryId.HasValue)
            {
                values = values.Where(x => x.CategoryId == categoryId).ToList();
            }
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.SelectedCategory = categoryId;

            return View(values);
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            ViewBag.Categories = new SelectList(
                _context.Categories.ToList(),
                "CategoryId",
                "CategoryName"
            );

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProject(Project project, IFormFile imageFile)
        {
            ModelState.Remove(nameof(Project.ImageUrl));
            ModelState.Remove(nameof(Project.Category));
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Lütfen proje bilgilerini kontrol ediniz.";

                ViewBag.Categories = new SelectList(
                    _context.Categories.ToList(),
                    "CategoryId",
                    "CategoryName",
                    project.CategoryId
                );

                return View(project);
            }

            string? error = ValidateImage(imageFile);

            if (error != null)
            {
                TempData["ErrorMessage"] = error;
                return RedirectToAction("CreateProject");
            }

            project.ImageUrl = await SaveImage(imageFile);

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Proje başarıyla eklendi.";

            return RedirectToAction("ProjectList");
        }
        [HttpGet]
        public IActionResult UpdateProject(int id)
        {

            var value = _context.Projects.Find(id);
            ViewBag.Categories = _context.Categories.ToList();
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProject(Project project, IFormFile? imageFile)
        {
            ModelState.Remove(nameof(Project.ImageUrl));
            ModelState.Remove(nameof(Project.Category));

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Lütfen proje bilgilerini kontrol ediniz.";
                ViewBag.Categories = _context.Categories.ToList();
                return View(project);
            }
            try
            {
                var value = _context.Projects.Find(project.ProjectId);

                if (imageFile != null)
                {
                    var error = ValidateImage(imageFile);

                    if (error != null)
                    {
                        TempData["ErrorMessage"] = error;
                        ViewBag.Categories = _context.Categories.ToList();
                        return View(project);
                    }

                    project.ImageUrl = await SaveImage(imageFile);
                }
                else
                {
                    project.ImageUrl = value.ImageUrl;
                }

                _context.Entry(value).CurrentValues.SetValues(project);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Proje başarıyla güncellendi!";
                return RedirectToAction("ProjectList");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Güncelleme sırasında bir hata oluştu!";
                ViewBag.Categories = _context.Categories.ToList();
                return View(project);
            }
        }

        [HttpPost]
        public IActionResult DeleteProject(int id)
        {
            var value = _context.Projects.Find(id);

             _context.Projects.Remove(value);
             _context.SaveChanges();
           

            return RedirectToAction("ProjectList");
        }
        private string? ValidateImage(IFormFile? imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return "Lütfen bir görsel seçiniz.";

            if (imageFile.Length > 5 * 1024 * 1024)
                return "Görsel boyutu 5 MB'tan büyük olamaz.";

            string extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

            if (!allowedExtensions.Contains(extension))
                return "Yalnızca JPG, PNG ve WEBP görseller yüklenebilir.";

            return null;
        }
        private async Task<string> SaveImage(IFormFile imageFile)
        {
            string extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            string fileName = Guid.NewGuid() + extension;

            string folder = Path.Combine("wwwroot", "images", "projects");
            Directory.CreateDirectory(folder);

            string path = Path.Combine(folder, fileName);

            using var stream = new FileStream(path, FileMode.CreateNew);
            await imageFile.CopyToAsync(stream);

            return "/images/projects/" + fileName;
        }
    }
}
