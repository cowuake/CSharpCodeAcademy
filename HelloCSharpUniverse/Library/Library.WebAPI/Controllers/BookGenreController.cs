using Library.Core.Interface;
using Library.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookGenreController : ControllerBase
    {
        private readonly IMainBusinessLogic _logic;

        public BookGenreController(IMainBusinessLogic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BookGenre>))]
        public IActionResult GetAllBookGenres()
        {
            var result = _logic.GetAllBookGenres();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BookGenre))]

        public IActionResult GetBookGenre(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid book genre ID");

            var result = _logic.GetBookGenre(id);

            if (result == null)
                return NotFound("Book genre not found");

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookGenre))]
        [ProducesResponseType(500, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public IActionResult CreateBookCategory([FromBody] BookGenre bookGenre)
        {
            if (bookGenre == null)
                return BadRequest("Invalid book genre data");

            var result = _logic.AddBookGenre(bookGenre);

            if (!result.Success)
                return StatusCode(500, "Cannot save book genre data: " + result.Message);

            return CreatedAtAction(nameof(GetBookGenre), new { id = bookGenre.Id }, bookGenre);

        }

    }
}