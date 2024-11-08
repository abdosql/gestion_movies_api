namespace mastering_.NET_API.Dtos
{
    public class UpdateMovieDto
    {
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime DateDeSortie { get; set; }
        public int Duree { get; set; }
        public List<Langue> Langues { get; set; }
        public string Pays { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile Cover { get; set; }
        public string Url { get; set; }
        public string Realisateur { get; set; }
        public byte GenreId { get; set; }
    }
}
