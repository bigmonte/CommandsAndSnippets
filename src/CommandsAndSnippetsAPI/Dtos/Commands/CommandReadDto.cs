namespace CommandsAndSnippetsAPI.Dtos
{
    public sealed class CommandReadDto
    {
        public int Id { get; init; }
        
        public string HowTo { get; init; }
        
        public string Platform { get; init; }
        
        public string CommandLine { get; init; }
        
    }
}