using MapTo;

namespace CommandsAndSnippets2.Models
{
    [MapFrom(typeof(Category))]
    public partial class CategoryCreate
    {
        public string Id { get; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get;  } = DateTime.Now;
    }
}

