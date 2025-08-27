using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using LinkUpApp.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkUpApp.API.Data;

public class Seed
{

    public async static Task SeedUsers(DataContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

        if (users is null) return;

        foreach (var user in users)
        {
            using var hmac = new HMACSHA512();
            user.UserName = user.UserName.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
            user.PasswordSalt = hmac.Key;
            await context.Users.AddAsync(user);
        }

        await context.SaveChangesAsync();
    }
}
