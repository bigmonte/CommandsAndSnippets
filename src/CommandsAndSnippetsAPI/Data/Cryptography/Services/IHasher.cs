
using CommandsAndSnippetsAPI.Data.Cryptography;

namespace CommandsAndSnippetsAPI.Data
{
    public interface IHasher
    { 
        string CreateHash(string text, BaseCryptoItem.HashAlgorithm algorithm);
        string CreateHash(string text, string salt, BaseCryptoItem.HashAlgorithm algorithm);
        bool MatchesHash(string text, string hash);
    }
}