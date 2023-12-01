using IntroToBlazor.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntroToBlazor.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
    }
}
