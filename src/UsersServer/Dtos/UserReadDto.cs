namespace UsersServer.Dtos.User
{
    public sealed class UserReadDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
    }
}