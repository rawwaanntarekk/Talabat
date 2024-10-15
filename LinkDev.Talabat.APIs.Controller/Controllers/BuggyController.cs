using LinkDev.Talabat.APIs.Controllers.Base;
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
            return NotFound(new {StatusCode = 404, Message = "Not Found"}); // 404
        }

        [HttpGet("servererror")] // GET: /api/buggy/servererror
        public IActionResult GetServerError()
        {
            
            throw new Exception(); //500
        }

        [HttpGet("badrequest")] // GET: /api/buggy/badrequest
        public IActionResult GetBadRequest()
        {
            return BadRequest(new {StatusCode = 400 , Message = "Bad Request"}); //400
        }

        [HttpGet("unauthorized")] // GET: /api/buggy/badrequest
        public IActionResult GetUnauthorized()  // GET: /api/buggy/unauthorized
        {
            return BadRequest(new { StatusCode = 401, Message = "Unauthorized" }); //401
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
