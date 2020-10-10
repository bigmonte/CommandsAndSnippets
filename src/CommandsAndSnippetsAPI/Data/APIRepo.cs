using System;
using System.Collections.Generic;
using System.Linq;
using CommandsAndSnippetsAPI.Models;

namespace CommandsAndSnippetsAPI.Data
{
    public class APIRepo : ICommandsAndSnippetsAPIRepo, ISnippetsAPIRepo
    {
        private readonly DBContext _dbContext;
        
        public APIRepo(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool SaveCommandsChanges()
        {
            // number of entities affected greater or equal to 0?
            return _dbContext.SaveChanges() >= 0;
        }

        public IEnumerable<Command> GetCommands()
        {
            return _dbContext.CommandItems.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _dbContext.CommandItems.FirstOrDefault(p => p.Id == id);
        }

        public void CreateCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            _dbContext.CommandItems.Add(command);
        }
        
        public IEnumerable<Command> GetCommandsWithPlatform(string platform)
        {
            var query = from c in GetCommands()
                where c.Platform == platform
                select c;

            return query;
        }

        public void UpdateCommand(Command command) { /* nothing here */ }

        public void DeleteCommand(Command command)
        {
            // TODO: make me 
            throw new System.NotImplementedException();
        }

        public bool SaveSnippetsChanges()
        {
            return _dbContext.SaveChanges() >= 0;
        }

        public IEnumerable<Snippet> GetSnippets()
        {
            return _dbContext.SnippetItems.ToList();
        }

        public IEnumerable<Snippet> GetSnippetsWithPlatform(string platform)
        {
            // TODO: make me 
            throw new NotImplementedException();
        }

        public Snippet GetSnippetById(int id)
        {
            // TODO: make me 
            throw new NotImplementedException();
        }

        public void CreateSnippet(Snippet snippet)
        {
            if (snippet == null)
            {
                throw new ArgumentNullException(nameof(snippet));
            }

            _dbContext.SnippetItems.Add(snippet);
        }

        public void UpdateSnippet(Snippet command) { /* nothing here */ }

        public void DeleteSnippet(Snippet command)
        {
            // TODO: make me 
            throw new NotImplementedException();
        }
    }
}