using System.ComponentModel.DataAnnotations;

namespace CommandsAndSnippetsAPI.Models
{
    // TODO?
    // Try use inheritance instead? (there's some caviats)
    // https://docs.microsoft.com/en-us/ef/core/modeling/inheritance

    // TODO: Modified Records
    
    public class Snippet
    {
        [Key]
        [Required]
        public int Id { get; set; }
    
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        
        [Required]
        public string Platform { get; set; }
        
        [Required]
        public string CodeSnippet { get; set; }
    }
}