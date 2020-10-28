
using UsersServer.Models;
using Microsoft.AspNetCore.Identity;

namespace UsersServer.Identities.Cryptography
{
    public interface IHasher : IPasswordHasher<User>
    { 
        string CreateHash(string text, BaseCryptoItem.HashAlgorithm algorithm);
        string CreateHash(string text, string salt, BaseCryptoItem.HashAlgorithm algorithm);
        bool MatchesHash(string text, string hash);
    }
}