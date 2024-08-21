using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OcharHasfarim.Models;
namespace OcharHasfarim.Data
{
    public class ApplicationDbContext : DbContext
    {
        //constractor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<OcharHasfarim.Models.Book> Book { get; set; } = default!;
        public DbSet<OcharHasfarim.Models.Library> Library { get; set; } = default!;
        public DbSet<OcharHasfarim.Models.Shelf> Shelf { get; set; } = default!;
    }
}