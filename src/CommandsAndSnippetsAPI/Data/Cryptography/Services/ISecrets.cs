namespace CommandsAndSnippetsAPI.Data.Cryptography
{
    public interface ISecrets
    {
        string GetSalt(string saltName);
    }
}