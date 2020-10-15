using System;
using System.Security.Cryptography;
using System.Text;

namespace CommandsAndSnippetsAPI.Data.Cryptography
{
    // ReSharper disable once InconsistentNaming
    public class SHA2_512 : BaseCryptoItem
    {
        public string Hash(string text, string salt, bool storeSalt)
        {
            var fullText = string.Concat(text, salt);
            var data = Encoding.UTF8.GetBytes(fullText);
            string hash;
            using SHA512 sha = new SHA512Managed();
            var hashBytes = sha.ComputeHash(data);
            var asString = ByteArrayToString(hashBytes);

            if (storeSalt)
            {
                hash = $"[{(int) HashAlgorithm.SHA3_512}]{salt}{asString}";
                return hash;
            }
                
            hash = $"[{(int) HashAlgorithm.SHA3_512}]{asString}";
            return hash;
        }
    }
}