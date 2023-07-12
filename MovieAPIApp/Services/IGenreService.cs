namespace MovieAPIApp.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetGenresAsync();

        Task<Genre> GetGenreByIdAsync(byte id);

        Task<Genre> AddGenreAsync(Genre genre);
        Genre DeleteGenreAsync(Genre genre);
        Genre UpdateGenreAsync(Genre genre);
        Task<bool> IsValid(byte id);
    }
}
