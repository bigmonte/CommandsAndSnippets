using MapTo;

namespace  CommandsAndSnippets2.Application.Users
{
    [MapFrom(typeof(ApplicationUserRole))]
    public partial class ApplicationUserRoleUnique
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string UserId { get; set; }

        public string RoleId { get; set; }
    }
}

