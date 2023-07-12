using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPIApp.Services;

namespace MovieAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genreService.GetGenresAsync();
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenreAsync(CreateGenreDto genreDto)
        {
            var genre = new Genre { 
                Name = genreDto.Name,
            };
           await _genreService.AddGenreAsync(genre);

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenreAsync(byte id, [FromBody] CreateGenreDto genreDto)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null) return NotFound($"No Genre was Found with ID : {id}");

            genre.Name = genreDto.Name;
            _genreService.UpdateGenreAsync(genre);  
            return Ok(genre);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGenreAsync(byte id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null) return NotFound($"No Genre was Found with ID : {id}");

            _genreService.DeleteGenreAsync(genre);
            return Ok(genre);
        }
    }
}
