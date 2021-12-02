using egRelationalDT.Data.Models;
using egRelationalDT.Data.ViewModels;

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
    }
}
