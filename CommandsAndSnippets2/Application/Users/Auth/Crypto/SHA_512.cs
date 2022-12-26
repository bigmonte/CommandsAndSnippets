using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace BlueWest.Cryptography
{
    /// <summary>
    /// SHA2_512 : BaseCryptoItem
    /// </summary>
    internal class SHA2_512 : BaseCryptoItem
    {
        /// <summary>
        /// Hash with the provided salt
        /// </summary>
        /// <param name="text"></param>
        /// <param name="salt"></param>
        /// <param name="storeSalt"></param>
        /// <returns></returns>
        public string Hash(string text, string salt, bool storeSalt)
        {
            var fullText = string.Concat(text, salt);
            var data = Encoding.UTF8.GetBytes(fullText);
            string hash;
            using SHA512 sha = SHA512.Create();
            var hashBytes = sha.ComputeHash(data);
            var asString = ByteArrayToString(hashBytes);

            if (storeSalt)
            {
                hash = $"{(int)HashAlgorithm.SHA3_512}{salt}{asString}";
                return hash;
            }

            hash = $"{(int)HashAlgorithm.SHA3_512}{asString}";
            return hash;
        }


        /// <summary>
        /// Hash_PBKDF2 algorithm.
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="salt"></param>
        /// <param name="saveSaltInResult"></param>
        /// <returns></returns>
        public string Hash_PBKDF2(string plainText, string salt, bool saveSaltInResult)
        {
            var saltAsBytes = Encoding.ASCII.GetBytes(salt);

            string hashed = ByteArrayToString(KeyDerivation.Pbkdf2(
                password: plainText,
                salt: saltAsBytes,
                prf: KeyDerivationPrf.HMACSHA512, //.NET 3.1 uses HMACSHA256 here
                iterationCount: 100000, //.NET 3.1 uses 10,000 iterations here
                numBytesRequested: 64)); //.NET 3.1 uses 32 bytes here

            if (saveSaltInResult)
                return string.Format("[{0}]{1}{2}", (int)HashAlgorithm.PBKDF2_SHA512, salt, hashed);
            else
                return string.Format("[{0}]{1}", (int)HashAlgorithm.PBKDF2_SHA512, hashed);
        }
    }
}