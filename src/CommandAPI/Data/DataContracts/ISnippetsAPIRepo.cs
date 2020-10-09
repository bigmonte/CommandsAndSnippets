using System.Collections.Generic;
using CommandAPI.Models;

namespace CommandAPI.Data
{
    // Repository Interface
    
    public interface ISnippetsAPIRepo
    {
        bool SaveSnippetsChanges();
        IEnumerable<Snippet> GetSnippets();
        IEnumerable<Snippet> GetSnippetsWithPlatform(string platform);
        Snippet GetSnippetById(int id);
        void CreateSnippet (Snippet command);
        void UpdateSnippet(Snippet command);
        void DeleteSnippet(Snippet command);
        
    }
}