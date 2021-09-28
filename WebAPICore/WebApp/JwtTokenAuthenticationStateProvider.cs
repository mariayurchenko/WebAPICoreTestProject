using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Repository;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebApp
{
    public class JwtTokenAuthenticationStateProvider: AuthenticationStateProvider
    {
        private readonly ITokenRepository _tokenRepository;

        public JwtTokenAuthenticationStateProvider(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenString = await _tokenRepository.GetToken();

            if (string.IsNullOrWhiteSpace(tokenString))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            if (tokenHandler.ReadToken(tokenString.Replace("\"", string.Empty)) is not JwtSecurityToken tokenJwt)
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var claims = new List<Claim>();
            claims.AddRange(tokenJwt.Claims);

            var nameClaim = tokenJwt.Claims.FirstOrDefault(x => x.Type == "unique_name");
            var roleClaim = tokenJwt.Claims.FirstOrDefault(x => x.Type == "role");
            if (nameClaim != null) claims.Add(new Claim(ClaimTypes.Name, nameClaim.Value));
            if (roleClaim != null) claims.Add(new Claim(ClaimTypes.Role, roleClaim.Value));

            var identity = new ClaimsIdentity(claims, "Custom Token Auth");
            var principal = new ClaimsPrincipal(identity);

            return new AuthenticationState(principal);

        }
    }
}