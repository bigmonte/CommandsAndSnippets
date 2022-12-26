using MapTo;

namespace  CommandsAndSnippets2.Application.Users
{
    [MapFrom(typeof(ApplicationUserClaim))]
    public partial class ApplicationUserClaimUnique
    {
        public  int Id { get; set; }
        public  string UserId { get; set; }
        public  string ClaimType { get; set; }
        public  string ClaimValue { get; set; }
    }
}

