using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Day12Test.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<Taxi> Taxi { get; set; }
    }
}
