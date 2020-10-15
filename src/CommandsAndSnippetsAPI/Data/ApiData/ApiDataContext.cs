using CommandsAndSnippetsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsAndSnippetsAPI.Data
{
    public sealed class ApiDataContext : DbContext
    {
        public ApiDataContext(DbContextOptions<ApiDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Command> CommandItems { get; set; }
        public DbSet<Snippet> SnippetItems { get; set; }

    }
}