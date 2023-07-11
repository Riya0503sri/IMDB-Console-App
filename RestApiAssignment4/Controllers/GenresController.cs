using Microsoft.AspNetCore.Mvc;
using RestApiAssignment4.Models.Requests;
using RestApiAssignment4.Models.Responses;
using RestApiAssignment4.Services.Interfaces;
using System;

namespace RestApiAssignment4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var genres = _genreService.Get();
            return Ok(genres);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var genre = _genreService.Get(id);
            if (genre == null)
            {
                return NotFound("Not found");
            }
            return Ok(genre);
        }

        [HttpPost]
        public IActionResult Create([FromBody] GenreRequest genreRequest)
        {
            int id;
            try
            {
                id= _genreService.Create(genreRequest);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("~/genres", new {id});
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] GenreRequest genreRequest)
        {
          
            try
            {
                _genreService.Update(id, genreRequest);
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
                _genreService.Delete(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
