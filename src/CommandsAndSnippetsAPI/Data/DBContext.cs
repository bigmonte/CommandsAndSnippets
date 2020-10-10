using CommandsAndSnippetsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsAndSnippetsAPI.Data
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