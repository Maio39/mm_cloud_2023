using Microsoft.EntityFrameworkCore;
using Day13Lab2.Models;
namespace Day13Lab2.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }

        public virtual DbSet<Zone> Zones { get; set; }
        public virtual DbSet<Temperature> Temperatures { get; set; }
        public DbSet<Humidity> Humidity { get; set; }
    }
}
