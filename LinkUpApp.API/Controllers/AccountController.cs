using System.Security.Cryptography;
using System.Text;
using LinkUpApp.API.Data;
using LinkUpApp.API.DTOs;
using LinkUpApp.API.Entities;
using LinkUpApp.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkUpApp.API.Controllers;

public class AccountController(DataContext Context,ITokenService tokenService) : BaseApiController
{

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        using var hmac = new HMACSHA512();

        if (await UserExists(registerDto.Username))
            return BadRequest("User is taken");

        AppUser user = new AppUser()
        {
            UserName = registerDto.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        Context.Users.Add(user);
        await Context.SaveChangesAsync();

           var userDto = new UserDto
        {
            UserName = user.UserName,
            Token = tokenService.CreateToken(user)
        };
        return userDto;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {

        var user = await Context.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username);

        if (user is null) return Unauthorized("Invalid username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        byte[] computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computeHash.Length; i++)
        {
            if (user.PasswordHash[i] != computeHash[i])
                return Unauthorized("Invalid Password");
        }

        var userDto = new UserDto
        {
            UserName = user.UserName,
            Token = tokenService.CreateToken(user)
        };
        
        return userDto;
        
    }

    private async Task<bool> UserExists(string userName)
    {
        return await Context.Users.AnyAsync(u => u.UserName.ToLower() == userName.ToLower());
    }

}
