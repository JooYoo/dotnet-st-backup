using egRelationalDT.Data.Models;
using egRelationalDT.Data.ViewModels;

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
    }
}
