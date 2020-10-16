using System.Threading.Tasks;
using CommandsAndSnippetsAPI.Models;

namespace CommandsAndSnippetsAPI.Data.Identities
{
    // TODO: Only allow login in the admin frontend 
    public interface ILoginManager
    {
        Task<bool> PasswordIsValid(User user, string password);
        
    }
}