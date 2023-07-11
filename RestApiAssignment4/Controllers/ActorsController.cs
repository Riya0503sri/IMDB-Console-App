using Microsoft.AspNetCore.Mvc;
using RestApiAssignment4.Models.Requests;
using RestApiAssignment4.Models.Responses;
using RestApiAssignment4.Services.Interfaces;
using System;

namespace RestApiAssignment4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;
        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var actors = _actorService.Get();
            return Ok(actors);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var actor = _actorService.Get(id);
            if (actor == null)
            {
                return NotFound("Not found");
            }
            return Ok(actor);

        }

        [HttpPost]
        public IActionResult Create([FromBody] ActorRequest actorRequest)
        {
            int id;
            try
            {

               id= _actorService.Create(actorRequest);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("~/actors", new { id });
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ActorRequest actorRequest)
        {
            try
            {
                _actorService.Update(id, actorRequest);
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
                _actorService.Delete(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
