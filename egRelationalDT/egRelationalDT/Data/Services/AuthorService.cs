using egRelationalDT.Data.Models;
using egRelationalDT.Data.ViewModels;
using System.Linq;

namespace egRelationalDT.Data.Services
{
    public class AuthorService
    {
        private AppDbContext _context { get; set; }
        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM authorVM)
        {
            var newAuthor = new Author()
            {
                FullName = authorVM.FullName
            };
            _context.Authors.Add(newAuthor);
            _context.SaveChanges();
        }

        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            var _author = _context.Authors.Where(author => author.Id == authorId).Select(n => new AuthorWithBooksVM()
            {
                FullName = n.FullName,
                BookTitles = n.Book_Authors.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();

            return _author;
        }
    }
}
