using System.Threading.Tasks;
using UsersServer.Dtos;

namespace UsersServer.Contracts
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName);
    }
}
