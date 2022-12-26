using MapTo;
using Microsoft.AspNetCore.Identity;
using  CommandsAndSnippets2.Models;

namespace  CommandsAndSnippets2.Application.Users
{
    /// <summary>
    /// Application User in the Identity System.
    /// </summary>
    [MapFrom(typeof(ApplicationUserUnique))]
    [UseUpdate]
    public partial class ApplicationUser : IdentityUser<string>
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public new string Id { get; set; }
        
        public List<Snippet> Snippets { get; set; }
        
        public List<Snippet> Favorites { get; set; }
        
    }
}