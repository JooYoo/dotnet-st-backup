using egRelationalDT.Data;
using egRelationalDT.Data.Models;
using egRelationalDT.Data.Services;
using egRelationalDT.Data.ViewModels;
using egRelationalDT.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace my_books_tests
{
    public class PublisherServiceTest
    {
        // create DbContextOptions: use in-memory db 
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbTest")
            .Options;


        AppDbContext context;

        // setup PublisherService dependency
        private PublisherService publisherService;


        [OneTimeSetUp]
        public void Setup()
        {
            // create Context
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            // seed test data
            SeedDatabase();

            // init PublisherService
            publisherService = new PublisherService(context);
        }


        // start Unit Test
        [Test, Order(1)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearchString_WithNoPageNumber()
        {
            var res = publisherService.GetAllPublishers("", "", null);
            // each Page maximal 5 items
            Assert.That(res.Count, Is.EqualTo(5));
        }

        [Test, Order(2)]
        public void GetAllPublishers_WithNoSortBy_WithNoSearchString_WithPageNumber()
        {
            var res = publisherService.GetAllPublishers("", "", 2);
            // each Page maximal 5 items
            Assert.That(res.Count, Is.EqualTo(2));
        }

        [Test, Order(3)]
        public void GetAllPublishers_WithNoSortBy_WithSearchString_WithNoPageNumber()
        {
            var res = publisherService.GetAllPublishers("", "3", null);
            // each Page maximal 5 items
            Assert.That(res.FirstOrDefault().Name, Is.EqualTo("Publisher 3"));
        }

        [Test, Order(4)]
        public void GetAllPublishers_WithSortBy_WithNoSearchString_WithNoPageNumber()
        {
            var res = publisherService.GetAllPublishers("name_desc", "", null);
            // each Page maximal 5 items
            Assert.That(res.FirstOrDefault().Name, Is.EqualTo("Publisher 7"));
        }

        [Test, Order(5)]
        public void GetPublisherById_WithId1()
        {
            var res = publisherService.GetPublisherById(1);

            Assert.That(res.Id, Is.EqualTo(1));
            Assert.That(res.Name, Is.EqualTo("Publisher 1"));
        }

        [Test, Order(6)]
        public void GetPublisherById_WithId7()
        {
            var res = publisherService.GetPublisherById(7);

            Assert.That(res.Name, Is.EqualTo("Publisher 7"));
        }

        [Test, Order(7)]
        public void GetPublisherById_WithNoResponse()
        {
            var res = publisherService.GetPublisherById(99);

            Assert.That(res, Is.Null);
        }


        [Test, Order(8)]
        public void AddPublisher_WithException()
        {
            // create a new Object
            var newPublisher = new PublisherVM()
            {
                Name = "123 with Exception"
            };

            // check the result
            Assert.That(() => publisherService.AddPublisher(newPublisher),
                Throws.Exception.TypeOf<PublisherNameException>().With.Message.EqualTo("Name starts with number"));
        }


        [Test, Order(9)]
        public void AddPublisher_WithNoException()
        {
            // create a new Object
            var newPublisher = new PublisherVM()
            {
                Name = "Without Exception"
            };
            // get result
            var res = publisherService.AddPublisher(newPublisher);
            // check result
            Assert.That(res, Is.Not.Null);
            Assert.That(res.Name, Does.StartWith("Without"));
            Assert.That(res.Id, Is.Not.Null);
        }

        [Test, Order(10)]
        public void GetPublisherData_Test()
        {
            var res = publisherService.GetPublisherData(1);

            Assert.That(res.Name, Is.EqualTo("Publisher 1"));
            Assert.That(res.BookAuthors, Is.Not.Empty);
            Assert.That(res.BookAuthors.Count, Is.GreaterThan(1));

            var firstBookName = res.BookAuthors.OrderBy(n => n.BookName).FirstOrDefault().BookName;
            Assert.That(firstBookName, Is.EqualTo("Book 1 Title"));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

        private void SeedDatabase()
        {
            var publishers = new List<Publisher>
            {
                    new Publisher() {
                        Id = 1,
                        Name = "Publisher 1"
                    },
                    new Publisher() {
                        Id = 2,
                        Name = "Publisher 2"
                    },
                    new Publisher() {
                        Id = 3,
                        Name = "Publisher 3"
                    },
                    new Publisher() {
                        Id = 4,
                        Name = "Publisher 4"
                    },
                    new Publisher() {
                        Id = 5,
                        Name = "Publisher 5"
                    },
                    new Publisher() {
                        Id = 6,
                        Name = "Publisher 6"
                    },
                     new Publisher() {
                        Id = 7,
                        Name = "Publisher 7"
                    },
            };
            context.Publishers.AddRange(publishers);

            var authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,
                    FullName = "Author 1"
                },
                new Author()
                {
                    Id = 2,
                    FullName = "Author 2"
                }
            };
            context.Authors.AddRange(authors);


            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Book 1 Title",
                    Description = "Book 1 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                },
                new Book()
                {
                    Id = 2,
                    Title = "Book 2 Title",
                    Description = "Book 2 Description",
                    IsRead = false,
                    Genre = "Genre",
                    CoverUrl = "https://...",
                    DateAdded = DateTime.Now.AddDays(-10),
                    PublisherId = 1
                }
            };
            context.Books.AddRange(books);

            var books_authors = new List<Book_Author>()
            {
                new Book_Author()
                {
                    Id = 1,
                    BookId = 1,
                    AuthorId = 1
                },
                new Book_Author()
                {
                    Id = 2,
                    BookId = 1,
                    AuthorId = 2
                },
                new Book_Author()
                {
                    Id = 3,
                    BookId = 2,
                    AuthorId = 2
                },
            };
            context.Book_Authors.AddRange(books_authors);


            context.SaveChanges();
        }
    }
}