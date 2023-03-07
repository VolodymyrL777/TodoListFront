using Microsoft.EntityFrameworkCore;
using TodoListBack.Models;

namespace TodoListBack.Repositories
{
    public class JobContext : DbContext
    {
        public DbSet<Job> Jobs => Set<Job>();
        public JobContext() => Database.EnsureCreated(); 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=todolist.db");
        }
    }
}
