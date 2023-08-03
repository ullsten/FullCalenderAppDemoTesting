using FullCalenderApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FullCalenderApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
        { 
        
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Color> Colors { get; set; }
    }
}

