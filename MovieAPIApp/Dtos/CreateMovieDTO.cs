namespace MovieAPIApp.Dtos
{
    public class CreateMovieDTO
    {
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(2500)]
        public string Description { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        public IFormFile? Poster { get; set; }
        public Byte GenreId { get; set; }
    }
}
