using System.Threading.Tasks;
using AutoMapper;
using LinkUpApp.API.Data;
using LinkUpApp.API.DTOs;
using LinkUpApp.API.Entities;
using LinkUpApp.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkUpApp.API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        var users = await userRepository.GetMembersAsync();
        return Ok(users);
    }

    [HttpGet("{userName}")]
    public async Task<ActionResult<MemberDto>> GetUser(string userName)
    {
        var user = await userRepository.GetMemberAsync(userName);
        if (user is null) return NotFound();
        return Ok(user);
    }
}
