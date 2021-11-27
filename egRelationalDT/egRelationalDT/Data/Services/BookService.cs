using egRelationalDT.Data.Models;
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
    }
}
