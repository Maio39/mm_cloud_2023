using Day10Lab1c;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Day10Lab1
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options) { }
        public virtual DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
