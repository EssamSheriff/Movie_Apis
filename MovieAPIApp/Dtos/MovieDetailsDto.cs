namespace MovieAPIApp.Dtos
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        public Byte[] Poster { get; set; }

        public String GenreName { get; set; }
        public Byte GenreId { get; set; }
    }
}
