namespace CommandsAndSnippetsAPI.Dtos.User
{
    public class AuthenticationDto
    {
        public string Token { get; init; }
        
        public string Success { get; init; }
        
        public string ErrorMessage { get; init; }
    }
}