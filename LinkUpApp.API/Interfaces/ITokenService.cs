using System;
using LinkUpApp.API.Entities;

namespace LinkUpApp.API.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser appUser);
}
