

using System.ComponentModel.DataAnnotations.Schema;

namespace mastering_.NET_API.Models
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [MaxLength(50,ErrorMessage = "Max Length is 50 caracter.")]
        public string Name { get; set; }

        public IList<Movie> Movies { get; set; }

    }
}
