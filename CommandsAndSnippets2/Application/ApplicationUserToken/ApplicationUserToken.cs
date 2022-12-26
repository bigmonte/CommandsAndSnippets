using Microsoft.AspNetCore.Identity;

namespace  CommandsAndSnippets2.Application.Users;

/// <inheritdoc />
public class ApplicationUserToken : IdentityUserToken<string>
{
    public string Id { get; set; }
}