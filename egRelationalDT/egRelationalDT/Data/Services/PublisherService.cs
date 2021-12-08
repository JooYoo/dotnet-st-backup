using egRelationalDT.Data.Models;
using egRelationalDT.Data.ViewModels;
using System.Linq;

namespace egRelationalDT.Data.Services
{
    public class PublisherService
    {
        private AppDbContext _context { get; set; }
        public PublisherService(AppDbContext context)
        {
            _context = context;
        }

        public void AddPublisher(PublisherVM publisherVM)
        {
            var newPublisher = new Publisher()
            {
                Name = publisherVM.Name
            };

            _context.Publishers.Add(newPublisher);
            _context.SaveChanges();
        }

        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _pubulisherData = _context.Publishers.Where(p => p.Id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return _pubulisherData;
        }

        public void DeletePublisherById(int publisherId)
        {
            var _publisher = _context.Publishers.FirstOrDefault(p => p.Id == publisherId);

            if (_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }

        }
    }
}
