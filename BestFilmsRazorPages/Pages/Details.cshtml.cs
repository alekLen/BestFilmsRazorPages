using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BestFilmsRazorPages.Models;
using BestFilmsRazorPages.Repository;

namespace BestFilmsRazorPages.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IFilmRepository _context;

        public DetailsModel(IFilmRepository context)
        {
            _context = context;
        }

        public Film Film { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }

            var film = await _context.GetFilm( id.Value);
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
    }
}
