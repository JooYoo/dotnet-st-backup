using egRelationalDT.ActionResults;
using egRelationalDT.Data.Models;
using egRelationalDT.Data.Services;
using egRelationalDT.Data.ViewModels;
using egRelationalDT.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;


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

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortby)
        {
            try
            {
                var res = _publisherService.GetAllPublishers(sortby);
                return Ok(res);
            }
            catch 
            {
                return BadRequest("No publishers data.");
               
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            try
            {
                var newPublisher = _publisherService.AddPublisher(publisher);
                return Created(nameof(AddPublisher), newPublisher);
            }
            catch (PublisherNameException ex)
            {
                // TEST: when the PublisherName starts with number thie Exception will be called
                return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("get-publisher-by-id/{id}")]
        //public IActionResult GetPublisherById(int id)
        //{
        //    // TEST: throw Exception by Middleware
        //    //throw new Exception("🥱 Exception handled by Middleware");

        //    var _res = _publisherService.GetPublisherById(id);

        //    if (_res != null)
        //    {
        //        return Ok(_res);
        //        //return _res;
        //    }
        //    else
        //    {
        //        return NotFound();
        //        //return null;
        //    }
        //}

        [HttpGet("get-publisher-by-id/{id}")]
        public CustomActionResult GetPublisherById(int id)
        {
            var _res = _publisherService.GetPublisherById(id);

            if (_res != null)
            {
                var _responseObj = new CustomActionResultVM()
                {
                    Publisher = _res
                };

                return new CustomActionResult(_responseObj);
            }
            else
            {
                var _responseObj = new CustomActionResultVM()
                {
                    Exception = new Exception("CustomActionResult: not found item")
                };

                return new CustomActionResult(_responseObj);

            }
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
            try
            {
                // TEST: bad arithmetic
                //int x1 = 1;
                //int x2 = 0;
                //int res = x1 / x2;

                _publisherService.DeletePublisherById(id);
                return Ok();
            }
            catch (ArithmeticException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
