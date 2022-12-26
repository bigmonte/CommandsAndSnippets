namespace CommandsAndSnippets2.Models
{
    public class Content
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public ContentType ContentType { get; set; } 
        public int Order { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        public Snippet Snippet { get; set; }
        public string SnippetId { get; set; }

    }
}

