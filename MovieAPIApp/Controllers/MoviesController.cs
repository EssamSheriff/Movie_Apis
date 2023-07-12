using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPIApp.Dtos;
using MovieAPIApp.Models;
using MovieAPIApp.Services;
using System.Xml.Linq;

namespace MovieAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private new List<String> _allowedExtentions = new List<string> { ".png", ".jpg" };
        private long _MaxPosterSize = 1048576;

        private readonly IMapper _mapper;
        private readonly IMovieServices _movieServices;
        private readonly IGenreService _genreServices;
        public MoviesController(IMovieServices movieServices, IGenreService genreServices, IMapper mapper)
        {
            _movieServices = movieServices;
            _genreServices = genreServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var Movies = await _movieServices.GetMoivesAsync();
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(Movies);
            // if (!Movies.any()) return NotFound("No Movies in Database");          

            return Ok(data);
        }
        
        [HttpGet("ByGenreId")]
        public async Task<IActionResult> GetMoviesByGenreId(byte id)
        {
            var Movies = await _movieServices.GetMoivesAsync(id);
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(Movies);
            // if (!Movies.any()) return NotFound("No Movies in Database");          

            return Ok(data);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieByID(int id)
        {
            var m = await _movieServices.GetMoiveByIdAsync(id);

            if (m == null) return NotFound();

            var MovieForm = _mapper.Map<MovieDetailsDto>(m);
            // if (!Movies.any()) return NotFound("No Movies in Database");          

            return Ok(MovieForm);
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromForm] CreateMovieDTO movieDTO)
        {
            if (movieDTO.Poster == null) return BadRequest("Poster is Required");
            if (!_allowedExtentions.Contains(Path.GetExtension(movieDTO.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg Extantions are allowed!");

            if (movieDTO.Poster.Length > _MaxPosterSize)
                return BadRequest("Psoter msut be lower than 1 MB !!");

            var IsValidGenre = await _genreServices.IsValid(movieDTO.GenreId);
            if (!IsValidGenre)
                return BadRequest("Invalid Genre ID!!");

            using var DataStream = new MemoryStream();
            await movieDTO.Poster.CopyToAsync(DataStream);
            Console.WriteLine("********************************************************");

            var movie = _mapper.Map<Movie>(movieDTO);
            movie.Poster = DataStream.ToArray();
            Console.WriteLine($"MMMMM=   {movie.Name} " );
            await _movieServices.AddMoiveAsync(movie);
            Console.WriteLine("*******///////////////////////////////////");

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var m = await _movieServices.GetMoiveByIdAsync(id);

            if (m == null) return NotFound();

            _movieServices.DeleteGenreAsync(m);

            return Ok(m);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromForm] CreateMovieDTO movieDTO)
        {
            var movie = await _movieServices.GetMoiveByIdAsync(id);
            if (movie == null) return NotFound();
            var IsValidGenre = await _genreServices.IsValid(movieDTO.GenreId);
            if (!IsValidGenre)
                return BadRequest("Invalid Genre ID!!");

            if(movieDTO.Poster != null)
            {
                if (!_allowedExtentions.Contains(Path.GetExtension(movieDTO.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg Extantions are allowed!");

                if (movieDTO.Poster.Length > _MaxPosterSize)
                    return BadRequest("Psoter msut be lower than 1 MB !!");

                using var DataStream = new MemoryStream();
                await movieDTO.Poster.CopyToAsync(DataStream);
                movie.Poster = DataStream.ToArray();
            }
            movie.Name = movieDTO.Name;
            movie.Description = movieDTO.Description;
            movie.GenreId = movieDTO.GenreId;
            movie.Rate = movieDTO.Rate;
            movie.Year = movieDTO.Year;

            _movieServices.UpdateMoiveAsync(movie);
            return Ok(movie);
        }
    }
}
