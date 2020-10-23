using System;
using System.ComponentModel.DataAnnotations;

namespace CommandsAndSnippetsAPI.Models
{
    // TODO: Modified Records
    public class Command
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
        public string CommandLine { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime CreatedDate { get; set; }
    }
}