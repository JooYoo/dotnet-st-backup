using egRelationalDT.Data.Services;
using egRelationalDT.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace egRelationalDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private BookService _bookService { get; set; }
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("get-books")]
        public IActionResult GetBooks()
        {
            var books = _bookService.GetBooks();
            return Ok(books);
        }

        [HttpGet("get-book-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var theBook = _bookService.GetBookById(id);
            return Ok(theBook);
        }

        [HttpPost("add-book-with-authors")]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _bookService.AddBookWithAuthors(book);
            return Ok();
        }

        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] BookVM book)
        {
            var theBook = _bookService.UpdateBookById(id, book);
            return Ok(theBook);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            _bookService.DeleteBookById(id);

            return Ok();
        }
    }
}
