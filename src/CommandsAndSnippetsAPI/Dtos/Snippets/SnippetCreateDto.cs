using System;
using System.ComponentModel.DataAnnotations;

namespace CommandsAndSnippetsAPI.Dtos
{
    public class SnippetCreateDto 
    {
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        
        [Required]
        public string Platform { get; set; }
        
        [Required]
        public string CodeSnippet { get; set; }
        
        public DateTime CreatedDate { get; } = DateTime.Now;
    }
}