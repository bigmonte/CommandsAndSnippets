namespace CommandsAndSnippetsAPI.Dtos.User
{
    public class UserReadDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}