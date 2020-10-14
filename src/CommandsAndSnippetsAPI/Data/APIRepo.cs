using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandsAndSnippetsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsAndSnippetsAPI.Data
{
    public class APIRepo : ICommandsAndSnippetsAPIRepo, ISnippetsAPIRepo
    {
        private readonly ApiDataContext _apiDataContext;
        
        public APIRepo(ApiDataContext apiDataContext)
        {
            _apiDataContext = apiDataContext;
        }
        public async Task<bool> SaveCommandsChanges()
        {
            // number of entities affected greater or equal to 0?
            return await _apiDataContext.SaveChangesAsync() >= 0;
        }

        public async Task<List<Command>> GetCommands()
        {
            var db = _apiDataContext.CommandItems;
            return await db.ToListAsync();
        }

        public async Task<Command> GetCommandById(int id)
        {
            var db = _apiDataContext.CommandItems;
            return await db.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async void CreateCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var dbCommands = _apiDataContext.CommandItems;

            await dbCommands.AddAsync(command);
        }
        
        public async Task<IEnumerable<Command>> GetCommandsWithPlatform(string platform)
        {
            var query = from c in await GetCommands()
                where c.Platform == platform
                select c;

            return query.ToList();
        }
        
        public async Task<IEnumerable<Command>> SearchCommands(string text)
        {
            var q =
                from d in await GetCommands()
                where d.Platform.ToLower().Contains(text.ToLower())
                      || d.CommandLine.ToLower().Contains(text.ToLower())
                      || d.HowTo.ToLower().Contains(text.ToLower())
                select d;
            
            return q.ToList();
        }
        
        public void UpdateCommand(Command command) { /* nothing here */ }

        public void DeleteCommand(Command command)
        {
            if(command == null)
            {
              throw new ArgumentNullException(nameof(command));
            }

            _apiDataContext.CommandItems.Remove(command);
        }

        public bool SaveSnippetsChanges()
        {
            return _apiDataContext.SaveChanges() >= 0;
        }

        public IEnumerable<Snippet> GetSnippets()
        {
            return _apiDataContext.SnippetItems.ToList();
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

            _apiDataContext.SnippetItems.Add(snippet);
        }

        public void UpdateSnippet(Snippet command) { /* nothing here */ }

        public void DeleteSnippet(Snippet command)
        {
            // TODO: make me 
            throw new NotImplementedException();
        }
    }
}