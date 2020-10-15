
using System;
using CommandsAndSnippetsAPI.Data.Cryptography;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CommandsAndSnippetsAPI.Data
{
    public class Hasher : BaseCryptoItem, IHasher, IPasswordHasher<User>
    {
        private const int SaltLength = 64;
        
        private readonly ISecrets _secrets;

        public Hasher(ISecrets secrets)
        {
            _secrets = secrets;
        }
        
        public string CreateHash(string text, BaseCryptoItem.HashAlgorithm algorithm)
        {
            var salt = CreateRandomString(SaltLength);
            return CreateHash(text, salt, algorithm, true);
        }

        public string CreateHash(string text, string saltName, BaseCryptoItem.HashAlgorithm algorithm)
        {
            var salt = _secrets.GetSalt(saltName);
            return CreateHash(text, salt, algorithm, false);
        }

        private string CreateHash(string text, string salt, HashAlgorithm algorithm, bool storeSalt)
        {
            string hash;

            switch (algorithm)
            {
                case HashAlgorithm.SHA3_512:
                    var sha2 = new SHA2_512();
                    hash = sha2.Hash(text, salt, storeSalt);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return hash;
        }

        public bool MatchesHash(string text, string hash)
        {
            string salt = "";

            GetAlgorithm(hash, out var algoAsInt, out _, out _, out salt);

            if (!algoAsInt.HasValue) return false;

            var hashAlgorithm = (HashAlgorithm) algoAsInt.Value;
            var hashed = CreateHash(text, salt, hashAlgorithm, true);
            return hashed == hash;
        }

        public string HashPassword(User user, string password)
        {
            return CreateHash(password, HashAlgorithm.SHA3_512);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            var match = MatchesHash(providedPassword, hashedPassword);
            return match ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }

        private void GetAlgorithm(string cipherText, out int? algorithm, out int? keyIndex,
            out string trimmedCipherText, out string salt)
        {
            GetAlgorithm(cipherText, out algorithm, out keyIndex, out trimmedCipherText);
            if (algorithm.HasValue && trimmedCipherText.Length > SaltLength)
            {
                salt = trimmedCipherText.Substring(0, SaltLength);
                trimmedCipherText = trimmedCipherText.Substring(SaltLength);
            }
            else
            {
                salt = null;
            }
        }
    }
}