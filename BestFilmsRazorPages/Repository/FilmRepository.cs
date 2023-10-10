using BestFilmsRazorPages.Models;
using Microsoft.EntityFrameworkCore;

namespace BestFilmsRazorPages.Repository
{
    public class FilmRepository: IFilmRepository
    {
        private readonly FilmsContext _context;

        public FilmRepository(FilmsContext context)
        {
            _context = context;
        }
        public async Task<List<Film>> GetFilmsList()
        {
            return await _context.Films.ToListAsync();
        }
        public async Task<Film> GetFilm(int id)
        {
            return await _context.Films.FindAsync(id);
        }

        public async Task Create(Film f)
        {
            await _context.Films.AddAsync(f);
        }

        public void Update(FilmsContext f)
        {
            _context.Entry(f).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            Film? f = await _context.Films.FindAsync(id);
            if (f != null)
                _context.Films.Remove(f);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
