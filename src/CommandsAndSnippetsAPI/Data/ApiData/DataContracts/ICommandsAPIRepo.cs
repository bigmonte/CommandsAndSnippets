using System.Collections.Generic;
using System.Threading.Tasks;
using CommandsAndSnippetsAPI.Models;

namespace CommandsAndSnippetsAPI.Data
{
    // Repository Interface
    
    public interface ICommandsApiRepo
    {
        Task<bool> SaveCommandsChanges();
        Task<List<Command>> GetCommands();
        Task<IEnumerable<Command>> GetCommandsWithPlatform(string platform);
        Task<IEnumerable<Command>> SearchCommands(string text);

        Task<Command> GetCommandByIdAsync(int id);
        Command GetCommandById(int id);
        
        void CreateCommand (Command command);
        void UpdateCommand(Command command);
        void DeleteCommand(Command command);
        
    }
}