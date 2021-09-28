using System.Threading.Tasks;

namespace App.Repository
{
    public interface IAuthenticationRepository
    {
        Task<string> LoginAsync(string userName, string password);
        Task<string> GetUserInfoAsync(string token);
    }
}