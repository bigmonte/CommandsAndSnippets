using MapTo;

namespace  CommandsAndSnippets2.Application.Users
{
    [MapFrom(typeof(ApplicationRoleClaim))]
    public partial class ApplicationRoleClaimUnique
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}

