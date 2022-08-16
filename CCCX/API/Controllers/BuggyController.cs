using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("testauth")] //testauth is a testing method
        [Authorize] //Authorize is a class that authorizes a user
        public ActionResult<string> GetSecretText() //if everything works out well, it returns a string
        {
            return "secret stuff";
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest() //method that returns a not found error
        {
            var thing = _context.Products.Find(42); //finds a product with id 42?

            if (thing == null) return NotFound(new ApiResponse(404)); //if product is null, return not found error

            return Ok(); //if product is not null, return ok??
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError() //method that returns a server error
        {
            var thing = _context.Products.Find(42); //finds a product with id 42?

            var thingToReturn = thing.ToString(); //converts product to string

            return Ok(); //returns ok?
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest() //method that returns a bad request type error
        {
            return BadRequest(new ApiResponse(400)); //returns bad request error
        }

        [HttpGet("badrequest/{id}")] //badrequest/{id} is a method that returns a bad request type error using an id
        public ActionResult GetNotFoundRequest(int id) 
        {
            return Ok(); //OkResult means that the request was successful
        }
    }
}