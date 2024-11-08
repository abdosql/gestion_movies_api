namespace mastering_.NET_API.Dtos
{
    public class UpdateGenreDto
    {
        [MaxLength(50, ErrorMessage = "Max Length is 50.")]
        public string Name { get; set; }
    }
}
