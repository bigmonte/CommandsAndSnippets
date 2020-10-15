using CommandsAndSnippetsAPI.Data;

namespace CommandsAndSnippetsTools
{
    public class CryptoTest
    {
        private Hasher _hasher;

        public void EncryptPass()
        {
            _hasher = new Hasher(null);
           var hash =  _hasher.HashPassword(null, "password");

           System.Console.WriteLine(hash);

           var match = _hasher.MatchesHash("password", hash);
           
           System.Console.WriteLine(match);

        }
    }
}