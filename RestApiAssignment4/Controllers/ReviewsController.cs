using Microsoft.AspNetCore.Mvc;
using RestApiAssignment4.Models.Requests;
using RestApiAssignment4.Services.Interfaces;
using System;

namespace RestApiAssignment4.Controllers
{
    [Route("movies/{movieId:int}/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public IActionResult GetAll([FromRoute] int movieId)
        {
            try
            {
                var reviews = _reviewService.Get(movieId);
                return Ok(reviews);
            }
            catch (ArgumentException)
            {
                return NotFound("Not found");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id, [FromRoute] int movieId)
        {
            try
            {
                var review = _reviewService.Get(id, movieId);
                return Ok(review);
            }
            catch (ArgumentException)
            {
                return NotFound("Not found");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] ReviewRequest reviewRequest, [FromRoute] int movieId)
        {
            int id;
            try
            {
                id= _reviewService.Create(reviewRequest, movieId);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("~/reviews", new {id});
        }

        [HttpPut("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] ReviewRequest reviewRequest, [FromRoute] int movieId)
        {
            
            try
            {
                _reviewService.Update(id, reviewRequest, movieId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromRoute] int movieId)
        {
            
            try
            {
                _reviewService.Delete(id, movieId);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
