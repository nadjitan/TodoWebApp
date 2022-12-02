using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoWebApp.Models;

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
