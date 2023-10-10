using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BestFilmsRazorPages.Models;

namespace BestFilmsRazorPages.Pages
{
    public class CreateModel : PageModel
    {
        private readonly FilmsContext _context;
        IWebHostEnvironment _appEnvironment;

        [BindProperty]
        public IFormFile photo { get; set; } = default!;
        public CreateModel(FilmsContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Film Film { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Films == null || Film == null)
            {
                return Page();
            }
            if (photo == null)
                ModelState.AddModelError("", "вы не добавили постер");
            DateTime today = DateTime.Today;
            int currentYear = today.Year;
            if (Convert.ToInt32(Film.Year) < 1895 || Convert.ToInt32(Film.Year) > currentYear)
                ModelState.AddModelError("", "не корректный год");
            if (photo != null)
            {
                // Путь к папке Files
                string path = "/Posters/" + photo.FileName; // имя файла

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream); // копируем файл в поток
                }
                Film f = new();
                f.Name = Film.Name;
                f.Genre = Film.Genre;
                f.Director = Film.Director;
                f.Year = Film.Year;
                f.Story = Film.Story;
                f.Photo = path;
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Films.Add(f);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("./Index");
                    }
                    catch
                    {
                        return Page();
                    }
                }
                else
                    return Page();
            }
            return Page();
        }
    }
}
