using System.Threading.Tasks;
using CommandsAndSnippetsAPI.Identities.Contracts;
using CommandsAndSnippetsAPI.Models;

namespace CommandsAndSnippetsAPI.Identities.Managers
{
    public class LoginManager : ILoginManager
    {
        private readonly SignInManager _signInManager;

        public LoginManager(SignInManager signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task <bool> PasswordIsValid(User user, string password)
        {
           var result = await  _signInManager.CheckPasswordSignInAsync(user, password, false);
           if (result.Succeeded) return true;
           return false;
        }
    }
}