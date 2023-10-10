using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BestFilmsRazorPages.Models;
using BestFilmsRazorPages.Repository;

namespace BestFilmsRazorPages.Pages
{
    public class EditModel : PageModel
    {
        private readonly IFilmRepository _context;

        public EditModel(IFilmRepository context)
        {
            _context = context;
        }

        [BindProperty]
        public Film Film { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, string? ph )
        {
            if (ph == null)
            {
                if (id == null || _context == null)
                {
                    return NotFound();
                }

                var film = await _context.GetFilm(id.Value);
                if (film == null)
                {
                    return NotFound();
                }
                Film = film;
                return Page();
            }
            else
            {
                if (_context == null)
                {
                    return NotFound();
                }

                var film = await _context.GetFilm( id.Value);
                if (film == null)
                {
                    return NotFound();
                }
                film.Photo = ph;               
                Film = film;             
                return Page();
            }
        }     

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Update(Film);

            try
            {
                await _context.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(Film.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FilmExists(int id)
        {
            return _context.FilmExists( id);
        }
    }
}
