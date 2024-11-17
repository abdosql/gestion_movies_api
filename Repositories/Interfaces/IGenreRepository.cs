namespace mastering_.NET_API.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenreAsync();
        Task<Genre> GetGenreByIdAsync(int Id);
        Task<Genre> AddGenreAsync(Genre genre);
        void UpdateGenre(Genre genre);
        void DeleteGenre(Genre genre);

    }
}
