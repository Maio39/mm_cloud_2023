using Day10SerialKillerb;
using Microsoft.EntityFrameworkCore;

namespace Day10SerialKiller
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }
        public virtual DbSet<SerialKiller> SerialKillers { get; set; }
    }
}
