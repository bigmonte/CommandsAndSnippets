using CommandAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandAPI.Data
{
    public class CommandContext : DbContext
    {
        
        public CommandContext(DbContextOptions<CommandContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Command> CommandItems { get; set; }
    }
}