using  CommandsAndSnippets2.Application.Users;
using   CommandsAndSnippets2.Users;
using Microsoft.AspNetCore.Identity;

namespace BlueWest.Cryptography;

/// <summary>
/// IHasher contract
/// </summary>
public interface IHasher : IPasswordHasher<ApplicationUser>
{ 
    /// <summary>
    /// Create hash
    /// </summary>
    /// <param name="text"></param>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    string CreateHash(string text, BaseCryptoItem.HashAlgorithm algorithm);
    /// <summary>
    /// Create hash
    /// </summary>
    /// <param name="text"></param>
    /// <param name="salt"></param>
    /// <param name="algorithm"></param>
    /// <returns></returns>
    string CreateHash(string text, string salt, BaseCryptoItem.HashAlgorithm algorithm);
    /// <summary>
    /// MatchesHash
    /// </summary>
    /// <param name="text"></param>
    /// <param name="hash"></param>
    /// <returns></returns>
    bool MatchesHash(string text, string hash);
}