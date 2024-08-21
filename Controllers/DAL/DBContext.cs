using Microsoft.EntityFrameworkCore;
using OcharHasfarim.Models;

namespace OcharHasfarim.DAL
{
    public class DBContext : DbContext
    {
        public DBContext(string connectionString) : base(GetOptions(connectionString))
        {
            Database.EnsureCreated();
            if (Libraries.Count() == 0)
            {
                Seed();
            }
        }
        private void Seed()
        {
            Library library = new Library
            {
               Genre = "תורה"
            };
            Libraries.Add(library);
            SaveChanges();
        }
        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
        public DbSet<Library> Libraries { get; set; }

    }
}
