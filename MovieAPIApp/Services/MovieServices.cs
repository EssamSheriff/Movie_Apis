using Microsoft.EntityFrameworkCore;
using MovieAPIApp.Models;

namespace MovieAPIApp.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly ApplicationDbContext _context;

        public MovieServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> AddMoiveAsync(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }

        public Movie DeleteGenreAsync(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }

        public async Task<Movie> GetMoiveByIdAsync(int id)
        {
            var movie = await _context.moives.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);
            return  movie;
        }

        public async Task<IEnumerable<Movie>> GetMoivesAsync(byte GenreId = 0)
        {
            return await _context.moives.Where(m => m.GenreId == GenreId || GenreId == 0 ).OrderByDescending(m => m.Rate).Include(m => m.Genre).ToListAsync();
        }

        public Movie UpdateMoiveAsync(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
