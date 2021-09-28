using System.Security.Claims;
using System.Threading.Tasks;
using App.Repository;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebApp
{
    public class CustomTokenAuthenticationStateProvider: AuthenticationStateProvider
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IAuthenticationRepository _authenticationRepository;

        public CustomTokenAuthenticationStateProvider(ITokenRepository tokenRepository,
            IAuthenticationRepository authenticationRepository)
        {
            _tokenRepository = tokenRepository;
            _authenticationRepository = authenticationRepository;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userName = await _authenticationRepository.GetUserInfoAsync(await _tokenRepository.GetToken());
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var claim = new Claim(ClaimTypes.Name, userName);
                var identity = new ClaimsIdentity(new[] { claim }, "Custom Token Auth");
                var principal = new ClaimsPrincipal(identity);

                return new AuthenticationState(principal);
            }
            else
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
    }
}