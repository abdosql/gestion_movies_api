using mastering_.NET_API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mastering_.NET_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private MyContext _context;
        public GenreController(MyContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres() 
        {
            IList<Genre> genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre(CreatGenreDto genre)
        {
            Genre g = new Genre();
            g.Name = genre.Name;
            await this._context.AddAsync(g);
            await this._context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllGenres), new { id = g.Id }, g);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id,UpdateGenreDto newGenre)
        {
            Genre? g = await this._context.Genres.FirstOrDefaultAsync(g => g.Id == id);
            if (g == null)
            {
                return BadRequest();
            }

            g.Name = newGenre.Name;
            await this._context.SaveChangesAsync();
            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteGenre(int id)
        {
            Genre? g = await this._context.Genres.SingleOrDefaultAsync(g => g.Id == id);
            if (g == null)
            {
                return BadRequest();
            }

            this._context.Genres.Remove(g);
            await this._context.SaveChangesAsync();

            return NoContent();
        }
    }
    
}
