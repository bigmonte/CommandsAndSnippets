using System;
using System.Collections.Generic;
using System.Linq;
using CommandAPI.Models;

namespace CommandAPI.Data
{
    public class SqlCommandApiRepo : ICommandAPIRepo
    {
        private readonly CommandContext _context;

        public SqlCommandApiRepo(CommandContext context)
        {
            _context = context;
        }
        public bool SaveChanges()
        {
            // number of entities affected greater or equal to 0?
            return _context.SaveChanges() >= 0;
        }

        public IEnumerable<Command> GetCommands()
        {
            return _context.CommandItems.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.CommandItems.FirstOrDefault(p => p.Id == id);
        }

        public void CreateCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            _context.CommandItems.Add(command);
        }

        public void UpdateCommand(Command command) { }

        public void DeleteCommand(Command command)
        {
            throw new System.NotImplementedException();
        }
    }
}