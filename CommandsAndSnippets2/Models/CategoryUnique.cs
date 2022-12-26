using MapTo;

namespace CommandsAndSnippets2.Models
{
    [MapFrom(typeof(Category))]
    public partial class CategoryUnique
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

