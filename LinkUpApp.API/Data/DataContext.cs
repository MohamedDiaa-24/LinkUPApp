using System;
using LinkUpApp.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinkUpApp.API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<AppUser> Users { get; set; }
}
