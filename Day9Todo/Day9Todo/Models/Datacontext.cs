using Microsoft.EntityFrameworkCore;

namespace Day9Todo.Models
{
    public class Datacontext: Microsoft.EntityFrameworkCore.DbContext
    {
        public Datacontext(DbContextOptions<Datacontext> options):base(options) { }
        public virtual DbSet<Task> Tasks { get; set; }
    }
}
