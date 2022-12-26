using System.ComponentModel.DataAnnotations;
using MapTo;

namespace CommandsAndSnippets2.Models
{
    [MapFrom(new []{typeof(CategoryUnique), typeof(CategoryCreate)})]
    public partial class Category
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public List<Snippet> Snippets { get; set; }
    }  
}

