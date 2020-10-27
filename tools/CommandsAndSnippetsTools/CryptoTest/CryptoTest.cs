
using IdentitiesServer.Identities.Cryptography;

namespace IdentitiesServerTools
{
    public class CryptoTest
    {
        private Hasher _hasher;

        public void EncryptPass()
        {
            _hasher = new Hasher();
           var hash =  _hasher.HashPassword(null, "password");

           System.Console.WriteLine(hash);

           var match = _hasher.MatchesHash("password", hash);
           
           System.Console.WriteLine(match);

        }
    }
}