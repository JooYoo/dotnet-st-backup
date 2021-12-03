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

        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(book => book.Id == id);
        }

        public void AddBookWithAuthors(BookVM bookVM)
        {
            var newBook = new Book()
            {
                Title = bookVM.Title,
                Description = bookVM.Description,
                IsRead = bookVM.IsRead,
                DateRead = bookVM.IsRead ? bookVM.DateRead : null,
                Rate = bookVM.IsRead ? bookVM.Rate : null,
                Genre = bookVM.Genre,
                CoverUrl = bookVM.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = bookVM.PublisherId,
            };

            _context.Books.Add(newBook);
            _context.SaveChanges();

            foreach (var id in bookVM.AuthorIds)
            {
                var newBook_author = new Book_Author()
                {
                    BookId = newBook.Id,
                    AuthorId = id
                };

                _context.Book_Authors.Add(newBook_author);
                _context.SaveChanges();
            }
        }

        public Book UpdateBookById(int id, BookVM bookVM)
        {
            // find the book
            var theBook = _context.Books.FirstOrDefault(book => book.Id == id);

            // update data
            if (theBook != null)
            {
                theBook.Title = bookVM.Title;
                theBook.Description = bookVM.Description;
                theBook.IsRead = bookVM.IsRead;
                theBook.DateRead = bookVM.IsRead ? bookVM.DateRead : null;
                theBook.Rate = bookVM.IsRead ? bookVM.Rate : null;
                theBook.Genre = bookVM.Genre;
                theBook.CoverUrl = bookVM.CoverUrl;
                theBook.DateAdded = DateTime.Now;

                _context.SaveChanges();
            }

            return theBook;
        }

        public void DeleteBookById(int id)
        {
            // find the book
            var _book = _context.Books.FirstOrDefault(book => book.Id == id);

            // delete the book
            if (_book != null)
            {
                _context.Books.Remove(_book);

                _context.SaveChanges();
            }
        }
    }
}
