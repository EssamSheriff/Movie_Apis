namespace MovieAPIApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }

        [MaxLength(2500)]
        public string Description { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        public Byte[] Poster { get; set; }

        public Genre Genre { get; set;}
        public Byte GenreId { get; set;}
    }
}
