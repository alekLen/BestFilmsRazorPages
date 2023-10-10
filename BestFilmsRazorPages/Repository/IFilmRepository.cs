using BestFilmsRazorPages.Models;

namespace BestFilmsRazorPages.Repository
{
    public interface IFilmRepository
    {
        Task<List<Film>> GetFilmsList();
        Task<Film> GetFilm(int id);
        Task Create(Film f);
        void Update(FilmsContext f);
        Task Delete(int id);
        Task Save();
    }
}
