using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace UsersServer.Identities.Cryptography
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

        protected byte[] HexStringToByteArray(string stringInHexFormat)
        {
            var converted = Enumerable.Range(0, stringInHexFormat.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(stringInHexFormat.Substring(x, 2), 16))
                .ToArray();
            
            return converted;
        }

        protected string ByteArrayToString(byte[] bytes)
        {
            var sb = new StringBuilder();
            
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));
            }

            return sb.ToString();
        }

        protected string CreateRandomString(int length)
        {
            var rng = new RNGCryptoServiceProvider();
            var buffer = new byte[length /2];
            rng.GetBytes(buffer);
            var randomString = BitConverter.ToString(buffer).Replace("-", "");
            return randomString;
        }

        // TODO Refactor me  
        protected void GetAlgorithm(string cipherText, out int? algorithm, out int? keyIndex,
            out string trimmedCipherText)
        {
            algorithm = null;
            keyIndex = null;
            trimmedCipherText = cipherText;

            if (cipherText.Length <= 5 || cipherText[0] != '[') return;

            var cipherInfo = cipherText.Substring(1, cipherText.IndexOf(']') - 1).Split(",");
            
            if(int.TryParse(cipherInfo[0], out var foundAlgorithm))
            {
                algorithm = foundAlgorithm;
            }

            if (cipherInfo.Length == 2 && int.TryParse(cipherInfo[1], out var foundKeyIndex))
                keyIndex = foundKeyIndex;
            trimmedCipherText = cipherText.Substring(cipherText.IndexOf(']') + 1);
            
        }
    }
}