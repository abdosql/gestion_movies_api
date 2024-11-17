namespace mastering_.NET_API.Dtos
{
    public class CreateGenreDto
    {
        [MaxLength(50,ErrorMessage ="Max Length is 50.")]
        public string NameGenre { get; set; }
    }
}
