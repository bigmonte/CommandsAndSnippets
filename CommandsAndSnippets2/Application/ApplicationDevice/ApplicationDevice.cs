using System.Globalization;
using System.Net;

namespace  CommandsAndSnippets2.Application
{
    public class ApplicationDevice
    {
        public string Id { get; set; }
        public ClientOperatingSystem ClientOperatingSystem { get; set; }
        public ClientPlatformType ClientPlatformType { get; set; }
        public string Resolution { get; set; }
    }
}

