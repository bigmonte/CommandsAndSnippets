using System.ComponentModel.DataAnnotations;

namespace CommandsAndSnippetsAPI.Dtos
{
    public class CommandCreateDto 
    {
        [Required]
        [MaxLength(250)]
        public string HowTo { get; init; }
        
        [Required]
        public string Platform { get; init;  }
        
        [Required]
        public string CommandLine { get; init;  }
    }
}