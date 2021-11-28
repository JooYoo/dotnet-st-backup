using egRelationalDT.Data.Models;
using egRelationalDT.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace egRelationalDT.Data.Services
{
    public class BookService
    {
        private AppDbContext _context { get; set; }
        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public List<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public void AddBook(BookVM bookVM)
        {
            var newBook = new Book()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.IsRead?bookVM.DateRead:null,
                Rate = bookVM.IsRead?bookVM.Rate:null,
                Genre = bookVM.Genre,
                Author = bookVM.Author,
                CoverUrl = bookVM.CoverUrl,
                DateAdded = DateTime.Now
            };

            _context.Books.Add(newBook);
            _context.SaveChanges();
        }
    }
}
