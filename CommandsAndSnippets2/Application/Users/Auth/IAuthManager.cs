using System;
using System.Security.Claims;
using System.Threading.Tasks;
using  CommandsAndSnippets2.Application;
using Microsoft.AspNetCore.Identity;

namespace   CommandsAndSnippets2.Users;

/// <summary>
/// Auth manager contract interface.
/// </summary>
public interface IAuthManager
{
    /// <summary>
    /// CreateUserAsync
    /// </summary>
    /// <param name="signUpRequest"></param>
    /// <returns></returns>
    Task<IdentityResult> CreateUserAsync(SignUpRequest signUpRequest);

    Task<(ClaimsIdentity, bool)> DoLogin(LoginRequest loginRequest);





}