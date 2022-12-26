using MapTo;
namespace CommandsAndSnippets2.Models
{
    [MapFrom(typeof(Snippet))]
    public partial class SnippetUnique
    {
        public string Id { get; set; }
    
        public string Title { get; set; }
    
        public DateTime CreatedDate { get; set; }
        
        public SnippetStatus SnippetStatus { get; set; }
        
        public string UserId { get; set; }

        public List<Category> Categories { get; set; }

    }
}

