using mastering_.NET_API.Data;
using mastering_.NET_API.Dtos;
using mastering_.NET_API.Mappers;
using mastering_.NET_API.Models;
using mastering_.NET_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace mastering_.NET_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorage;

        public MovieController(IUnitOfWork unitOfWork, IFileStorageService fileStorage)
        {
            _unitOfWork = unitOfWork;
            _fileStorage = fileStorage;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _unitOfWork.Movies.GetAllAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromForm] AddMovieDto movieDto)
        {
            var movie = MovieMappers.getMovieFromAddMovieDto(movieDto);

            var (imageSuccess, imageName) = await _fileStorage.SaveFileAsync(movieDto.Image, "Uploads/MoviesImages");
            if (!imageSuccess)
            {
                ModelState.AddModelError("Image", "Failed to upload image");
                return ValidationProblem(ModelState);
            }
            movie.Image = imageName;

            var (coverSuccess, coverName) = await _fileStorage.SaveFileAsync(movieDto.Cover, "Uploads/MoviesCovers");
            if (!coverSuccess)
            {
                await _fileStorage.DeleteFileAsync(imageName, "Uploads/MoviesImages");
                ModelState.AddModelError("Cover", "Failed to upload cover");
                return ValidationProblem(ModelState);
            }
            movie.Cover = coverName;

            await _unitOfWork.Movies.AddAsync(movie);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromForm] UpdateMovieDto movieDto)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            var oldImage = movie.Image;
            var oldCover = movie.Cover;

            movie = MovieMappers.getMovieFromUpdateMovieDto(movie, movieDto);

            if (movieDto.Image != null)
            {
                var (imageSuccess, imageName) = await _fileStorage.SaveFileAsync(movieDto.Image, "Uploads/MoviesImages");
                if (!imageSuccess)
                {
                    ModelState.AddModelError("Image", "Failed to upload new image");
                    return ValidationProblem(ModelState);
                }
                movie.Image = imageName;
                await _fileStorage.DeleteFileAsync(oldImage, "Uploads/MoviesImages");
            }

            if (movieDto.Cover != null)
            {
                var (coverSuccess, coverName) = await _fileStorage.SaveFileAsync(movieDto.Cover, "Uploads/MoviesCovers");
                if (!coverSuccess)
                {
                    ModelState.AddModelError("Cover", "Failed to upload new cover");
                    return ValidationProblem(ModelState);
                }
                movie.Cover = coverName;
                await _fileStorage.DeleteFileAsync(oldCover, "Uploads/MoviesCovers");
            }

            await _unitOfWork.Movies.UpdateAsync(movie);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            await _fileStorage.DeleteFileAsync(movie.Image, "Uploads/MoviesImages");
            await _fileStorage.DeleteFileAsync(movie.Cover, "Uploads/MoviesCovers");

            await _unitOfWork.Movies.DeleteAsync(movie);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
