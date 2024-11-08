using System.ComponentModel.DataAnnotations.Schema;

namespace mastering_.NET_API.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime DateDeSortie { get; set; }
        public int Duree { get; set; } 
        public Langue Langue { get; set; }
        public string Pays { get; set; }
        public string Image { get; set; }
        public string Cover { get; set; }
        public string Url { get; set; }
        public string Realisateur { get; set; }

        [ForeignKey("Genre")] 
        public byte GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
