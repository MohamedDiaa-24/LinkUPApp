using LinkUpApp.API.Data;
using LinkUpApp.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LinkUpApp.API.Controllers;


public class BuggyController(DataContext dataContext) : BaseApiController
{

    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetAuth()
    {
        return "secret text";
    }

    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        var user = dataContext.Users.Find(-1);
        if (user is null) return NotFound();
        return user;
    }

    [HttpGet("server-error")]
    public ActionResult<AppUser> GetServerError()
    {
        var user = dataContext.Users.Find(-1) ?? throw new Exception("A bad thing has happened");
        return user;
    }
    [HttpGet("bad-request")]
    public ActionResult<AppUser> GetBadRequest()
    {
        return BadRequest("this is a not good request");
    }
    
    [HttpGet("validation-error/{number}")]
    public ActionResult<AppUser> GetValidationError(int number)
    {
        return Ok();
    }
}

