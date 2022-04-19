using Library.Core.Entities;
using Library.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Library.WebAPI.Controllers
{
    // Match the controller
    [Route("api/[controller]")] // To redirect to this controller, [...]/api/... as address
    [ApiController] // Not strictly needed unless we want to delegate actions to the controller
    public class LibraryController : ControllerBase
    {
        private readonly IMainBusinessLogic _logic;

        public LibraryController(IMainBusinessLogic logic) // Constructor injection
        {
            _logic = logic;
        }

        [HttpGet] // REST requires the action to be specified by the method
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Book>))]
        public IActionResult GetAllBooks()
        {
            var result = _logic.GetAllBooks();

            // View or HTTP request response (here we don't have views)
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Book))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddBook([FromBody] Book book)
        {
            if (book == null)
                return BadRequest("Invalid book data.");

            var result = _logic.AddBook(book);

            if (!result.Success)
                return StatusCode(500, "Cannot insert book.");

            return NoContent(); // 204
        }

        [HttpGet("{isbn}")] // api/library/N
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult GetBook(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                return BadRequest("Invalid book ISBN."); // Error 400

            var result = _logic.GetBook(isbn);

            if (result == null)
                return NotFound("Book not found."); // Error 404

            return Ok(result);
        }

        [HttpDelete("{isbn}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult RemoveBook(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
                return BadRequest("Invalid employee id."); // Error 400

            var result = _logic.RemoveBookByISBN(isbn);

            if (!result.Success)
                return StatusCode(500, "Cannot remove book.");

            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public IActionResult UpdateBook(string isbn, Book book)
        {
            if (string.IsNullOrEmpty(isbn) || book == null)
                return BadRequest("Invalid parameters.");

            if (isbn != book.ISBN)
                return BadRequest("Book's ISBN does not match.");

            var result = _logic.UpdateBook(book);

            if (!result.Success)
                return StatusCode(500, "Cannot update book data.");

            return Ok(book);
        }
    }
}