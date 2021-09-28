using System.Threading.Tasks;
using App.Repository;

namespace App.ApplicationLogic
{
    public class AuthenticationUseCases : IAuthenticationUseCases
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly ITokenRepository _tokenRepository;

        public AuthenticationUseCases(IAuthenticationRepository authenticationRepository,
            ITokenRepository tokenRepository)
        {
            _authenticationRepository = authenticationRepository;
            _tokenRepository = tokenRepository;
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            return await _authenticationRepository.LoginAsync(userName, password);
        }

        public async Task<string> GetUserInfoAsync(string token)
        {
            return await _authenticationRepository.GetUserInfoAsync(token);
        }

        public async Task Logout()
        {
            await _tokenRepository.SetToken(string.Empty);
        }
    }
}