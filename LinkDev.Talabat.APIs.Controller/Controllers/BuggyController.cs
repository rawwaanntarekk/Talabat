using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers
{
    public class BuggyController : BaseAPIController
    {
        [HttpGet("notfound")] // GET: /api/buggy/notfound
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(new ApiResponse(404)); // 404
        }

        [HttpGet("servererror")] // GET: /api/buggy/servererror
        public IActionResult GetServerError()
        {
            
            throw new Exception(); //500
        }

        [HttpGet("badrequest")] // GET: /api/buggy/badrequest
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400)); //400
        }

        [HttpGet("unauthorized")] // GET: /api/buggy/badrequest
        public IActionResult GetUnauthorized()  // GET: /api/buggy/unauthorized
        {
            return BadRequest(new  ApiResponse(401)); //401
        }

        [HttpGet("badrequest/{id}")] // GET: /api/buggy/badrequest/five
        public IActionResult GetValidationError(int id) // => 400
        {
            return Ok();
        }

        [HttpGet("forbidden")] // GET: /api/buggy/forbidden
        public IActionResult GetForbidden() // => 401
        {
            return Forbid();
        }


        [Authorize]
        [HttpGet("autherized")] // GET: /api/buggy/autherized
        public IActionResult GetAuthorizedRequest()
        {
            return Ok();
        }

    }
}
