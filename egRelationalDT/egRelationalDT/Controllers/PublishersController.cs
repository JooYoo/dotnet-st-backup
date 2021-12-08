using egRelationalDT.Data.Services;
using egRelationalDT.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace egRelationalDT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        public PublisherService _publisherService { get; set; }
        public PublishersController(PublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            _publisherService.AddPublisher(publisher);
            return Ok();
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var _res = _publisherService.GetPublisherData(id);
            return Ok(_res);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            _publisherService.DeletePublisherById(id);
            return Ok();
        }
    }
}
