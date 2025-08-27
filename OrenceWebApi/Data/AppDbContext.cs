using Microsoft.EntityFrameworkCore;
using OrenceApi.Models;
using System.Collections.Generic;

namespace OrenceApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
    }
}