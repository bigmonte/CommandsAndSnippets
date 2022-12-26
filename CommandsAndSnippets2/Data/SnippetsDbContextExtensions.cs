using CommandsAndSnippets2.Models;

namespace CommandsAndSnippets2.Data
{
    public static partial class SnippetsDbContextExtensions
    {
        /// <summary>
        /// Adds a new Snippet and returns a projection of type CommandsAndSnippets2.Models.SnippetUnique.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="snippetToCreate">Projection data of Snippet</param>
        /// <returns>The added data.</returns>
        public static (bool, SnippetUnique) AddSnippet(this SnippetsDbContext dbContext, SnippetCreate snippetToCreate) 
        {
            var existingCategories = (from cat in snippetToCreate.Categories let existingCategory = dbContext.Categories.Find(cat.Id) select existingCategory ?? cat).ToList();
            snippetToCreate.Categories = existingCategories;
            var newSnippet = new Snippet(snippetToCreate);
            dbContext.Snippets.Add(newSnippet);
            var success = dbContext.SaveChanges() >= 0;
            return (success, new SnippetUnique(newSnippet));
        }
    }
}

