using System.ComponentModel.DataAnnotations;

namespace  CommandsAndSnippets2.Application
{
    
    public class ApplicationDeviceCreate
    {
        [Required] public string Uuid;
        public string Languages { get; set; }
        public string UserAgent { get; set; }
        public OperatingSystemType OperatingSystemType { get; set; }
    }
}

