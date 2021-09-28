using System.Threading.Tasks;
using App.Repository.ApiClient;

namespace App.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;
        private readonly ITokenRepository _tokenRepository;

        public AuthenticationRepository(IWebApiExecuter webApiExecuter, ITokenRepository tokenRepository)
        {
            _webApiExecuter = webApiExecuter;
            _tokenRepository = tokenRepository;
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            var token = await _webApiExecuter.InvokePostReturnString("authenticate", new { userName = userName, password = password });

            await _tokenRepository.SetToken(token);

            if (string.IsNullOrWhiteSpace(token) || token == "\"\"") return null;

            return token;
        }

        public async Task<string> GetUserInfoAsync(string token)
        {
            var userName = await _webApiExecuter.InvokePostReturnString("getuserinfo", new { token = token });

            if (string.IsNullOrWhiteSpace(userName) || userName == "\"\"") return null;

            return userName;
        }
    }
}