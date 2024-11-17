using mastering_.NET_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace mastering_.NET_API.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private MyContext _context;
        public GenreRepository(MyContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllGenreAsync()
        {
            return await _context.Genres.OrderBy(g => g.Name).ToListAsync();
        }


        public async Task<Genre> GetGenreByIdAsync(int Id)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.Id == Id);
        }


        public async Task<Genre> AddGenreAsync(Genre genre)
        {
            await this._context.AddAsync(genre);
            await this._context.SaveChangesAsync();
            return genre;
        }


        public void DeleteGenre(Genre genre)
        {
            this._context.Remove(genre);
            this._context.SaveChanges();
        }


        public void UpdateGenre(Genre genre)
        {
            this._context.Update(genre);
            this._context.SaveChanges();
        }
    }
}
