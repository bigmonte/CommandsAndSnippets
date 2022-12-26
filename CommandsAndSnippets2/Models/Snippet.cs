using System.Diagnostics.CodeAnalysis;
using CommandsAndSnippets2.Application.Users;
using MapTo;
namespace CommandsAndSnippets2.Models
{
    /// <summary>
    /// Represents a Snippet post
    /// </summary>
    [MapFrom(new []{typeof(SnippetCreate), typeof(SnippetUnique)})]
    public partial class Snippet
    {
        public string Id { get; set; }
    
        public string Title { get; set; }
    
        public DateTime CreatedDate { get; set; }
        
        public SnippetStatus SnippetStatus { get; set; }
        
        public List<Content> Contents { get; set; }
        
        public ApplicationUser User { get; set; }
        
        public string UserId { get; set; }
        public List<Category> Categories { get; set; }


    }
}

