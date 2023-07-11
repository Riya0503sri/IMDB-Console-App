using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiAssignment4.Models.Requests;
using RestApiAssignment4.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;
using System;

namespace RestApiAssignment4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var movies = _movieService.Get();
            return Ok(movies);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var movie = _movieService.Get(id);
            if (movie == null)
            {
                return NotFound("Not found");
            }
            return Ok(movie);
        }

        [HttpPost]
        public IActionResult Create([FromBody] MovieRequest movieRequest)
        {
            int id; 
            try
            {
                id = _movieService.Create(movieRequest);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            } 
            return Created("~/movies", new { id });
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] MovieRequest movieRequest)
        {
            
            try
            {
                _movieService.Update(id, movieRequest);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            
            try
            {
                _movieService.Delete(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("{id}/poster")]
        public async Task<ActionResult> UpdatePoster([FromRoute] int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");
            var client = new FirebaseStorage("imdb-e7620.appspot.com")
                .Child(Guid.NewGuid().ToString() + ".jpg");
            var downloadUrl = await client.Child(Path.GetFileName(file.FileName)).PutAsync(file.OpenReadStream());
           
            return Ok(downloadUrl);
        }
    }
}
