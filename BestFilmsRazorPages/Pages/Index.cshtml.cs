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
    public class IndexModel : PageModel
    {
        private readonly FilmsContext _context;

        public IndexModel(FilmsContext context)
        {
            _context = context;
        }

        public IList<Film> Film { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context != null)
            {
                Film = await _context.Films.ToListAsync(); 
            }
        }
    }
}
