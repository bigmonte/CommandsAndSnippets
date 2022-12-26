using MapTo;
namespace CommandsAndSnippets2.Models
{
    [MapFrom(typeof(Snippet))]
    public partial class SnippetCreate
    {
        public string Title { get; set; }
    
        public DateTime CreatedDate { get; set; }
        
        public List<Category> Categories { get; set; }

        public SnippetStatus SnippetStatus { get; set; } 
    }
}

