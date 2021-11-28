using egRelationalDT.Data.Models;
using egRelationalDT.Data.Services;
using egRelationalDT.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace egRelationalDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BookService _bookService { get; set; }
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

        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody]BookVM book)
        {
            _bookService.AddBook(book);
            return Ok();
        }
    }
}
