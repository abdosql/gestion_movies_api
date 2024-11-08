using mastering_.NET_API.Models;
using Microsoft.EntityFrameworkCore;

namespace mastering_.NET_API.Data
{
    public class MyContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            
        }
    }
}
