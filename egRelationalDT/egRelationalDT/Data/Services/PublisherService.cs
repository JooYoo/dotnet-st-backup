using egRelationalDT.Data.Models;
using egRelationalDT.Data.ViewModels;
using egRelationalDT.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace egRelationalDT.Data.Services
{
    public class PublisherService
    {
        private AppDbContext _context { get; set; }
        public PublisherService(AppDbContext context)
        {
            _context = context;
        }

        public List<Publisher> GetAllPublishers(string sortby)
        {
            // default sort, based on the key
            var allPublishers = _context.Publishers.OrderBy(n => n.Name).ToList();

            // sortby publisher-name
            if (!string.IsNullOrEmpty(sortby))
            {
                switch (sortby)
                {
                    case "name_desc":
                        allPublishers = allPublishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            return allPublishers;
        }

        public Publisher AddPublisher(PublisherVM publisherVM)
        {
            // throw exception when input starts with number
            if (StringStartsWithNumber(publisherVM.Name))
            {
                throw new PublisherNameException("Name starts with number", publisherVM.Name);
            }

            var newPublisher = new Publisher()
            {
                Name = publisherVM.Name
            };

            _context.Publishers.Add(newPublisher);
            _context.SaveChanges();

            return newPublisher;
        }

        public Publisher GetPublisherById(int id)
        {
            return _context.Publishers.FirstOrDefault(p => p.Id == id);
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
            else
            {
                throw new Exception($"The publisher with id: {publisherId} not exist");
            }

        }

        private bool StringStartsWithNumber(string name)
        {
            return Regex.IsMatch(name, @"^\d");
        }
    }
}
