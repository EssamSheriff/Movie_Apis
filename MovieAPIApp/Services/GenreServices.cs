using Microsoft.EntityFrameworkCore;
using MovieAPIApp.Models;

namespace MovieAPIApp.Services
{
    public class GenreServices : IGenreService
    {


        private readonly ApplicationDbContext _context;

        public GenreServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> AddGenreAsync(Genre genre)
        {
            await _context.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre DeleteGenreAsync(Genre genre)
        {
            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return genre;
        }

        public async Task<Genre> GetGenreByIdAsync(byte id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            return await _context.Genres.OrderBy(g => g.Name).ToListAsync();
        }

        public async Task<bool> IsValid(byte id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }

        public Genre UpdateGenreAsync(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();
            return genre;
        }
    }
}
