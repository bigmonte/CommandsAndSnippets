
namespace CommandsAndSnippetsAPI.Identities.Cryptography
{
    public interface IHasher
    { 
        string CreateHash(string text, BaseCryptoItem.HashAlgorithm algorithm);
        string CreateHash(string text, string salt, BaseCryptoItem.HashAlgorithm algorithm);
        bool MatchesHash(string text, string hash);
    }
}