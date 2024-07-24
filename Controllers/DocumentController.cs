using Microsoft.AspNetCore.Mvc;
using Xceed.Words.NET;
using WebApplication18.Models;
using WebApplication18.Context;

namespace WebApplication18.Controllers
{
    public class DocumentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DocumentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DocumentModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();

                string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "template.docx");
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "output.docx");

                CreateWordDocument(templatePath, outputPath, model);

                byte[] fileBytes = System.IO.File.ReadAllBytes(outputPath);
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "output.docx");
            }

            return View(model);
        }

        private void CreateWordDocument(string templatePath, string outputPath, DocumentModel model)
        {
            using (DocX document = DocX.Load(templatePath))
            {
                document.ReplaceText("{FirstName}", model.FirstName);
                document.ReplaceText("{LastName}", model.LastName);
                document.ReplaceText("{Address}", model.Address);

                document.SaveAs(outputPath);
            }
        }
    }
}
