namespace MovieService.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Director { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public double Price { get; set; }
        public double Duration { get; set; }
        public string Language { get; set; }
        public int AgeRestiction { get; set; }
        public string Description { get; set; }


    }
}
