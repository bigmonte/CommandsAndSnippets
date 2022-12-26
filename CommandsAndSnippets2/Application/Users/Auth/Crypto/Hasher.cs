using System;
using  CommandsAndSnippets2.Application.Users;
using   CommandsAndSnippets2.Users;
using Microsoft.AspNetCore.Identity;


namespace BlueWest.Cryptography
{
    
    /// <summary>
    /// Hasher
    /// </summary>
    public class Hasher : BaseCryptoItem, IHasher
    {
        private const int SaltLength = 64;
        
        /// <summary>
        /// CreateHash
        /// </summary>
        /// <param name="text"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public string CreateHash(string text, HashAlgorithm algorithm)
        {
            var salt = CreateRandomString(SaltLength);
            return CreateHash(text, salt, algorithm, true);
        }

        /// <summary>
        /// CreateHash
        /// </summary>
        /// <param name="text"></param>
        /// <param name="saltName"></param>
        /// <param name="algorithm"></param>
        /// <returns></returns>
        public string CreateHash(string text, string saltName, BaseCryptoItem.HashAlgorithm algorithm)
        {
            return CreateHash(text, saltName, algorithm, false);
        }

        private string CreateHash(string text, string salt, HashAlgorithm algorithm, bool storeSalt)
        {
            string hash;

            switch (algorithm)
            {
                case HashAlgorithm.SHA2_512:
                    var sha2 = new SHA2_512();
                    hash = sha2.Hash(text, salt, storeSalt);
                    break;
                case HashAlgorithm.SHA3_512:
                    var sha3 = new SHA2_512();
                    hash = sha3.Hash(text, salt, storeSalt);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return hash;
        }

        /// <summary>
        /// Check for a matching hash.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public bool MatchesHash(string text, string hash)
        {
            string salt = "";

            GetAlgorithm(hash, out var algoAsInt, out _, out _, out salt);

            if (!algoAsInt.HasValue) return false;

            var hashAlgorithm = (HashAlgorithm) algoAsInt.Value;
            var hashed = CreateHash(text, salt, hashAlgorithm, true);
            return hashed == hash;
        }

        /// <summary>
        /// Hash password
        /// </summary>
        /// <param name="ApplicationUser"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(ApplicationUser ApplicationUser, string password)
        {
            return CreateHash(password, HashAlgorithm.SHA3_512);
        }

        public PasswordVerificationResult VerifyHashedPassword(ApplicationUser ApplicationUser, string hashedPassword,
            string providedPassword)
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