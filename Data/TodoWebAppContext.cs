using Microsoft.EntityFrameworkCore;

namespace TodoWebApp.Data
{
    public class TodoWebAppContext : DbContext
    {
        public TodoWebAppContext (DbContextOptions<TodoWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<TodoWebApp.Models.Todo> Todo { get; set; } = default!;
    }
}
