using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinkUpApp.API.DTOs;
using LinkUpApp.API.Entities;
using LinkUpApp.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkUpApp.API.Data;

public class UserRepository(DataContext context,IMapper mapper) : IUserRepository
{
    public async Task<MemberDto?> GetMemberAsync(string name)
    {
        return await context.Users
        .Where(x => x.UserName == name)
        .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return  await context.Users
         .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
         .ToListAsync();
    }

    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<AppUser?> GetUserByUserNameAsync(string name)
    {
        return await context.Users
        .Include(x => x.Photos)
        .FirstOrDefaultAsync(x=>x.UserName == name);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {
        return await context.Users
        .Include(x => x.Photos)
        .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Update(AppUser user)
    {
        context.Entry(user).State = EntityState.Modified;
    }
}
