using BestFilmsRazorPages.Models;
using BestFilmsRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BestFilmsRazorPages.Pages
{
    public class PosterModel : PageModel
    {
        private readonly IFilmRepository _context;
        IWebHostEnvironment _appEnvironment;
        [BindProperty]
        public Film Film { get; set; } = default!;
        [BindProperty]
        public IFormFile photo { get; set; } = default!;
        public PosterModel(IFilmRepository context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }
        public async Task<IActionResult> OnGetAsync()
        {          
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (Film.Id == null || _context == null)
            {
                return NotFound();
            }
            if(photo==null)
            {
                return RedirectToPage("./Edit", new {  id=Film.Id });
            }
            var f = await _context.GetFilm(Film.Id);
            if (f == null)
            {
                return NotFound();
            }
            string ph = "/Posters/" + photo.FileName;
            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + f.Photo, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream); // копируем файл в поток
            }
           // string ph = f.Photo;
            int id = f.Id;
         
            return RedirectToPage("./Edit", new { ph, id });
            //  return RedirectToPage("./Edit/ToEdit", new { ph, id });
        }
    }
}
