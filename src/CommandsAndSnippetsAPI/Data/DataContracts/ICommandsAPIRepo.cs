using System.Collections.Generic;
using CommandsAndSnippetsAPI.Models;

namespace CommandsAndSnippetsAPI.Data
{
    // Repository Interface
    
    public interface ICommandsAndSnippetsAPIRepo
    {
        bool SaveCommandsChanges();
        IEnumerable<Command> GetCommands();
        IEnumerable<Command> GetCommandsWithPlatform(string platform);
        Command GetCommandById(int id);
        void CreateCommand (Command command);
        void UpdateCommand(Command command);
        void DeleteCommand(Command command);
        
    }
}