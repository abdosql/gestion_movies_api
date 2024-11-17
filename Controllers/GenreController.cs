using AutoMapper;
using Azure;
using mastering_.NET_API.Dtos;
using mastering_.NET_API.Models;
using mastering_.NET_API.Repositories;
using mastering_.NET_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace mastering_.NET_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<string> _logger;


        public GenreController(IGenreRepository genreRepository, IMapper mapper, ILogger<string> logger)
        {
            this._genreRepository = genreRepository;
            _mapper = mapper;
            _logger = logger;
        }

        


        [HttpGet]
        public async Task<IActionResult> GetAllGenres() 
        {
            IEnumerable<Genre> genres = await _genreRepository.GetAllGenreAsync();
            _logger.LogError("heelloooooooooooo");
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre(CreateGenreDto genre)
        {
            Genre g = _mapper.Map<Genre>(genre);

            await _genreRepository.AddGenreAsync(g);

            return CreatedAtAction(nameof(GetAllGenres), new { id = g.Id }, g);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id,UpdateGenreDto newGenre)
        {
            Genre? g = await _genreRepository.GetGenreByIdAsync(id);
            if (g == null)
            {
                return BadRequest();
            }

            g.Name = newGenre.Name;
            _genreRepository.UpdateGenre(g);
            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteGenre(int id)
        {
            Genre? g = await _genreRepository.GetGenreByIdAsync(id);
            if (g == null)
            {
                return BadRequest();
            }

            _genreRepository.DeleteGenre(g);

            return NoContent();
        }
    }
    
}
