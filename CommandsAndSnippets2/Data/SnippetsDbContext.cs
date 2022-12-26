using CommandsAndSnippets2.Models;
using Microsoft.EntityFrameworkCore;
using BlueWest.EfMethods;

namespace CommandsAndSnippets2.Data
{
    
    [EfGenerator]
    public class SnippetsDbContext : DbContext
    {
        /// <summary>
        /// Data-access to the available Snippets.
        /// </summary>
        [EfGetMany(typeof(SnippetUnique))]
        [EfGetOneBy(nameof(Snippet.Id), typeof(SnippetUnique))]
        public  DbSet<Snippet> Snippets { get; set; }
        
        /// <summary>
        /// Snippet Categories.
        /// </summary>
        [EfGetMany(typeof(CategoryUnique))]
        [EfAddMethods(typeof(CategoryCreate), typeof(CategoryUnique))]
        public  DbSet<Category> Categories { get; set; }
        
        public  DbSet<Content> Contents { get; set; }
        
        

        /// <inheritdoc />
        public SnippetsDbContext(DbContextOptions<SnippetsDbContext> options) : base(options)
        {
        }
        
        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureCurrentDbModel();

        }

    }
}

