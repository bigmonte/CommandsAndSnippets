using System.Collections.Generic;
using System.Threading.Tasks;
using CommandsAndSnippetsAPI.Models;

namespace CommandsAndSnippetsAPI.Data
{
    // Repository Interface
    
    public interface ISnippetsAPIRepo
    {
        bool SaveSnippetsChanges();
        IEnumerable<Snippet> GetSnippets();
        IEnumerable<Snippet> GetSnippetsWithPlatform(string platform);
        Task<IEnumerable<Snippet>> SearchSnippets(string text);
        Snippet GetSnippetById(int id);
        void CreateSnippet (Snippet snippet);
        void UpdateSnippet(Snippet snippet);
        void DeleteSnippet(Snippet snippet);
        
    }
}