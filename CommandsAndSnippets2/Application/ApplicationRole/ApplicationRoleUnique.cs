using MapTo;

namespace  CommandsAndSnippets2.Application.Users
{
    [MapFrom(typeof(ApplicationRole))]
    public partial class ApplicationRoleUnique
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}

