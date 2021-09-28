using System.Threading.Tasks;

namespace App.Repository
{
    public interface ITokenRepository
    {
        Task SetToken(string token);
        Task<string> GetToken();
    }
}