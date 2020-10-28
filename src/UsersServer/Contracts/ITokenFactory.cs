namespace UsersServer.Contracts
{
    public interface ITokenFactory
    {
        string GenerateToken(int size= 32);
    }
}
