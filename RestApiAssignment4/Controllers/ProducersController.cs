using Microsoft.AspNetCore.Mvc;
using RestApiAssignment4.Models.Requests;
using RestApiAssignment4.Services.Interfaces;
using System;

namespace RestApiAssignment4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService producerService)
        {
            _producerService = producerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var producers = _producerService.Get();
            return Ok(producers);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var producer = _producerService.Get(id);
            if (producer == null)
            {
                return NotFound("Not found");
            }
            return Ok(producer);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProducerRequest producer)
        {
            int id;
            try
            {
                id= _producerService.Create(producer);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("~/producers", new { id });
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ProducerRequest producer)
        {

            try
            {
                _producerService.Update(id, producer);
            }
            catch (ArgumentException  ex)
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
                _producerService.Delete(id);
            }
            catch (ArgumentException)
            {
                return NotFound();
            } 
            return Ok();
        }
    }
}
