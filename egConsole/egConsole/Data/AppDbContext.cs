using egConsole.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace egConsole.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<GameConsole> GameConsoles { get; set; }
    }
}
