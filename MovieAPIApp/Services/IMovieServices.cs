namespace MovieAPIApp.Services
{
    public interface IMovieServices
    {
        Task<IEnumerable<Movie>> GetMoivesAsync(byte GenreId =0 );

        Task<Movie> GetMoiveByIdAsync(int id);

        Task<Movie> AddMoiveAsync(Movie Moive);
        Movie DeleteGenreAsync(Movie Moive);
        Movie UpdateMoiveAsync(Movie Moive);
    }
}
