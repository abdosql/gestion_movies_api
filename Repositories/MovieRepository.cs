using mastering_.NET_API.Data;
using mastering_.NET_API.Models;
using mastering_.NET_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace mastering_.NET_API.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MyContext _context;

        public MovieRepository(MyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .OrderBy(m => m.Titre)
                .ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Movie> AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            return movie;
        }

        public async Task UpdateAsync(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Movie movie)
        {
            _context.Movies.Remove(movie);
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Movies.AnyAsync(m => m.Id == id);
        }
    }
} 