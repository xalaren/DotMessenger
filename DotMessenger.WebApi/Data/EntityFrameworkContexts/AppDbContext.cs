using DotMessenger.Core.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotMessenger.WebApi.Data.EntityFrameworkContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}