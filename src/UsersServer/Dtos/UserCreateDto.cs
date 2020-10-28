namespace UsersServer.Dtos
{
    public sealed class UserCreateDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}