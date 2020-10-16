using System.Threading.Tasks;
using CommandsAndSnippetsAPI.Models;

namespace CommandsAndSnippetsAPI.Data.Identities
{
    public interface ILoginManager
    {
        Task<bool> PasswordIsValid(User user, string password);
        
    }
}