namespace BlueWest.Cryptography
{
    public interface ISessionHasher
    {
        /// <summary>
        /// Generates a token for the current session
        /// </summary>
        void GenerateSessionToken();
        
    }
}

