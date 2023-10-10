using BestFilmsRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BestFilmsRazorPages.Pages
{
    public class PosterModel : PageModel
    {
        private readonly FilmsContext _context;
        IWebHostEnvironment _appEnvironment;
        [BindProperty]
        public Film Film { get; set; } = default!;
        [BindProperty]
        public IFormFile photo { get; set; } = default!;
        public PosterModel(FilmsContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Films == null)
            {
                return NotFound();
            }

            var film = await _context.Films.FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }
            else
            {
                Film = film;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (Film.Id == null || _context.Films == null)
            {
                return NotFound();
            }
            var f = await _context.Films.FindAsync(Film.Id);
            if (f == null)
            {
                return NotFound();
            }
            f.Photo = "/Posters/" + photo.FileName;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + f.Photo, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream); // копируем файл в поток
            }
            string ph = f.Photo;
            int id = f.Id;
           return RedirectToPage("./Edit", new { ph, id });
          //  return RedirectToPage("./Edit/ToEdit", new { ph, id });
        }
    }
}
