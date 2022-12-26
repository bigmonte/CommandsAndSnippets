using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BlueWest.Cryptography
{
    public abstract class BaseCryptoItem
    {
        public enum HashAlgorithm
        {
            // ReSharper disable once InconsistentNaming
            SHA2_512 = 1,

            // ReSharper disable once InconsistentNaming
            PBKDF2_SHA512 = 2,

            // ReSharper disable once InconsistentNaming
            SHA3_512 = 3
        }

        /// <summary>
        /// HexStringToByteArray
        /// </summary>
        /// <param name="stringInHexFormat"></param>
        /// <returns></returns>
        protected byte[] HexStringToByteArray(string stringInHexFormat)
        {
            var converted = Enumerable.Range(0, stringInHexFormat.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(stringInHexFormat.Substring(x, 2), 16))
                .ToArray();

            return converted;
        }

        /// <summary>
        /// ByteArrayToString
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        protected string ByteArrayToString(byte[] bytes)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Generates a random string
        /// </summary>
        /// <param name="length">The length of the random string</param>
        /// <returns></returns>
        protected string CreateRandomString(int length)
        {
            var rng = RandomNumberGenerator.Create();
            var buffer = new byte[length / 2];
            rng.GetBytes(buffer);
            var randomString = BitConverter.ToString(buffer).Replace("-", "");
            return randomString;
        }

        // TODO Refactor me  
        /// <summary>
        /// Get Cryptographic algorithm
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="algorithm"></param>
        /// <param name="keyIndex"></param>
        /// <param name="trimmedCipherText"></param>
        protected void GetAlgorithm(string cipherText, out int? algorithm, out int? keyIndex,
            out string trimmedCipherText)
        {
            algorithm = null;
            keyIndex = null;
            trimmedCipherText = cipherText;

            if (cipherText.Length <= 5 ) return;

            var cipherInfo = cipherText[0].ToString();

            if (int.TryParse(cipherInfo, out int foundAlgorithm))
            {
                algorithm = foundAlgorithm;
            }

            if (int.TryParse(cipherInfo, out int foundKeyIndex))
                keyIndex = foundKeyIndex;
            
            trimmedCipherText = cipherText.Substring(cipherText.IndexOf(cipherInfo, StringComparison.Ordinal) + 1);
        }
    }
}