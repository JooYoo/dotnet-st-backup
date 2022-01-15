using egRelationalDT.Controllers;
using egRelationalDT.Data;
using egRelationalDT.Data.Models;
using egRelationalDT.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace my_books_tests
{
    class PublisherControllerTests
    {
        // init Tests
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbControllerTest")
            .Options;

        AppDbContext context;

        // setup PublisherController dependency
        private PublisherService publisherService;
        private PublishersController publishersController;

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
            publishersController = new PublishersController(publisherService, new NullLogger<PublishersController>());
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

       


            context.SaveChanges();
        }


    }
}
