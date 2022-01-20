using egRelationalDT.ActionResults;
using egRelationalDT.Controllers;
using egRelationalDT.Data;
using egRelationalDT.Data.Models;
using egRelationalDT.Data.Services;
using egRelationalDT.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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

        [Test, Order(1)]
        public void HTTPGET_GetAllPublishers_WithSortBy_Search_PageNr_Test()
        {
            IActionResult actionResult = publishersController.GetAllPublishers("name_desc", "Publisher", 1);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            var actionResultData = (actionResult as OkObjectResult).Value as List<Publisher>;
            Assert.That(actionResultData.First().Name, Is.EqualTo("Publisher 6"));
            Assert.That(actionResultData.First().Id, Is.EqualTo(6));
            Assert.That(actionResultData.Count, Is.EqualTo(5));

            IActionResult actionResultSecondPage = publishersController.GetAllPublishers("name_desc", "Publisher", 2);
            Assert.That(actionResultSecondPage, Is.TypeOf<OkObjectResult>());
            var actionResultDataSecondPage = (actionResultSecondPage as OkObjectResult).Value as List<Publisher>;
            Assert.That(actionResultDataSecondPage.First().Name, Is.EqualTo("Publisher 1"));
            Assert.That(actionResultDataSecondPage.First().Id, Is.EqualTo(1));
            Assert.That(actionResultDataSecondPage.Count, Is.EqualTo(1));
        }

        [Test, Order(2)]
        public void HTTPGET_GetPublisherById_WithId_FoundData_Test()
        {
            IActionResult actionResult = publishersController.GetPublisherById(1);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var actionResData = (actionResult as OkObjectResult).Value as Publisher;
            Assert.That(actionResData.Name, Is.EqualTo("publisher 1").IgnoreCase);
        }

        [Test, Order(3)]
        public void HTTPGET_GetPublisherById_WithId_NotFoundData_Test()
        {
            IActionResult notFoundActionRes = publishersController.GetPublisherById(100);
            Assert.That(notFoundActionRes, Is.TypeOf<NotFoundResult>());
        }

        [Test, Order(4)]
        public void HTTPPOST_AddPublisher_ReturnsCreated_Test()
        {
            // create a new publisher
            var newPublisher = new PublisherVM()
            {
                Name = "New Publisher"
            };

            // get a ActionResult
            IActionResult actionRes = publishersController.AddPublisher(newPublisher);

            // assert result
            Assert.That(actionRes, Is.TypeOf<CreatedResult>());
        }

        [Test, Order(5)]
        public void HTTPPOST_AddPublisher_ReturnsBadRequest_Test()
        {
            // create a new publisher
            var newPublisher = new PublisherVM()
            {
                Name = "123 New Publisher"
            };

            // get a ActionResult
            IActionResult actionRes = publishersController.AddPublisher(newPublisher);

            // assert result
            Assert.That(actionRes, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test, Order(6)]
        public void HTTPDELETE_DeletePublisherById_ReturnOk_Test()
        {
            // test para
            int publisherId = 6;
            // test case
            IActionResult actionResult = publishersController.DeletePublisherById(publisherId);
            // res
            Assert.That(actionResult, Is.TypeOf<OkResult>());
        }

        [Test, Order(7)]
        public void HTTPDELETE_DeletePublisherById_ReturnBad_Test()
        {
            // test para
            int publisherId = 6;
            // test case
            IActionResult actionResult = publishersController.DeletePublisherById(publisherId);
            // res
            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
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
                    }
            };
            context.Publishers.AddRange(publishers);

            context.SaveChanges();
        }

    }
}
