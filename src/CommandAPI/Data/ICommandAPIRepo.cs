using System.Collections.Generic;
using CommandAPI.Models;

namespace CommandAPI.Data
{
    // Repository Interface
    
    public interface ICommandAPIRepo
    {
        bool SaveChanges();
        IEnumerable<Command> GetCommands();
        Command GetCommandById(int id);
        void CreateCommand (Command command);
        void UpdateCommand(Command command);
        void DeleteCommand(Command command);
        
    }
}