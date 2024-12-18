using mastering_.NET_API.Repositories.Interfaces;

namespace mastering_.NET_API.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IGenreRepository Genres { get; }
        IMovieRepository Movies { get; }
        Task<int> CompleteAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _context;
        public IGenreRepository Genres { get; private set; }
        public IMovieRepository Movies { get; private set; }

        public UnitOfWork(MyContext context, IGenreRepository genreRepository, IMovieRepository movieRepository)
        {
            _context = context;
            Genres = genreRepository;
            Movies = movieRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
} 