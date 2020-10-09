using System.Collections.Generic;
using CommandAPI.Models;

namespace CommandAPI.Data
{
    // Repository Interface
    
    public interface ICommandAPIRepo
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