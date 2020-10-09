using CommandAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandAPI.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Command> CommandItems { get; set; }
        public DbSet<Snippet> SnippetItems { get; set; }
        
    }
}