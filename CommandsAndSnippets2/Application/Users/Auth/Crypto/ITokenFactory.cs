namespace   CommandsAndSnippets2.Users;

internal interface ITokenFactory
{
    string GenerateToken(int size= 32);
}