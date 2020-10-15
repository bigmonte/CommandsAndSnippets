using System.Security.Principal;
using System.Threading.Tasks;
using CommandsAndSnippetsAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace CommandsAndSnippetsAPI.Data.Identities
{
    public interface IUserRepo : IUserStore<User>
    { }
}