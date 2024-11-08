using mastering_.NET_API.Dtos;
using mastering_.NET_API.Mappers;
using mastering_.NET_API.Models;
using mastering_.NET_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mastering_.NET_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private MyContext _context;
        private FileUpload _fileUpload;



        public MovieController(MyContext context, FileUpload fileUpload)
        {
            this._context  = context;
            this._fileUpload = fileUpload;
        }




        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            IList<Movie> movies = await this._context.Movies.Include(m => m.Genre).ToListAsync();
            return Ok(movies);
        }





        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            Movie? movie = await this._context.Movies.Include(m => m.Genre).FirstOrDefaultAsync(m => m.Id == id);
            if(movie == null)
            {
                return BadRequest();
            }
            return Ok(movie);
        }





        [HttpPost]
        public async Task<IActionResult> AddMovie(AddMovieDto movie)
        {
            Movie Newmovie = MovieMappers.getMovieFromAddMovieDto(movie);

            this._fileUpload.UploadImage(movie.Image, "Uploads/MoviesImages");
            if (this._fileUpload.uploadState.State)
            {
                Newmovie.Image = this._fileUpload.uploadState.PhotoName;
            }

            this._fileUpload.UploadImage(movie.Cover, "Uploads/MoviesCovers");
            if (this._fileUpload.uploadState.State)
            {
                Newmovie.Cover = this._fileUpload.uploadState.PhotoName;
            }

            await this._context.AddAsync(Newmovie);
            await this._context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovieById), new { id = Newmovie.Id }, Newmovie);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, UpdateMovieDto mv)
        {

            Movie? movie = await this._context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return BadRequest();
            }

            Movie Newmovie = MovieMappers.getMovieFromUpdateMovieDto(movie, mv);
            this._fileUpload.DeleteImage(movie.Image, "Uploads/MoviesImages");
            this._fileUpload.DeleteImage(movie.Cover, "Uploads/MoviesCovers");

            this._fileUpload.UploadImage(mv.Image, "Uploads/MoviesImages");
            if (this._fileUpload.uploadState.State)
            {
                movie.Image = this._fileUpload.uploadState.PhotoName;
            }

            this._fileUpload.UploadImage(mv.Cover, "Uploads/MoviesCovers");
            if (this._fileUpload.uploadState.State)
            {
                movie.Cover = this._fileUpload.uploadState.PhotoName;
            }
            await this._context.SaveChangesAsync();
            return NoContent();
        }





        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            Movie? m = await this._context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            if (m == null)
            {
                return BadRequest();
            }

            this._fileUpload.DeleteImage(m.Image, "Uploads/MoviesImages");
            this._fileUpload.DeleteImage(m.Cover, "Uploads/MoviesCovers");

            this._context.Movies.Remove(m);
            await this._context.SaveChangesAsync();

            return NoContent();
        }
    }
}
