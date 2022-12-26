using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace   CommandsAndSnippets2.Users;

public static class AuthConsts
{
    /// <summary>
    /// Helper object to return a negative callback
    /// </summary>
    public static (ClaimsIdentity, bool) NegativeToken => (null, false);
    
    
}