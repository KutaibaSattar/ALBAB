using ALBAB.Entities.AppAccounts;
using ALBAB.Entities.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBAB.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BuggyController : BaseController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;

        }

        [Authorize]
     
        [HttpGet("auth")] //401 UnAuthorize Responses
        public ActionResult<string> GetSecret()
        {
                return "Secret text";
            
        }
       
         [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFount()
        {
               var thing = _context.Users.Find(-1); //not exist

               if (thing ==null) 
               {
                    return NotFound("Not found");
               }
           
                
               return Ok(thing); // sure not reached here

            
        }
         [HttpGet("server-error")] // internal server error 500
        public ActionResult<string> GetServerError()
        {
                var thing = _context.Users.Find(-1); // return Null
               
                 // generate error
                var thingToReturn = thing.ToString(); //Null.ToString Generate error

                return thingToReturn; // sure not reached here
              
            
        }
       
        [HttpGet("bad-request")]
       public ActionResult GetBadRequest()
        {
                return BadRequest("Baad request"); //(new ApiResponse(400));
            
        }

        
       [HttpGet("bad-request/{id}")] // Validation Error passing string instead of integer
       public ActionResult GetBadRequest(int id)
        {
             return Ok();   
            
        }



    }
}