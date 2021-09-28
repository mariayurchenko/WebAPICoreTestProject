using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace WebApi.Auth
{
    public class JwtTokenManager : ICustomTokenManager
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly byte[] _secrectKey;

        public JwtTokenManager(IConfiguration configration)
        {
            _tokenHandler = new JwtSecurityTokenHandler();
            _secrectKey = Encoding.ASCII.GetBytes(configration.GetValue<string>("JwtSecretKey"));
        }

        public string CreateToken(string userName)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userName));

            if (userName == "tom")
            {
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(_secrectKey),
                        SecurityAlgorithms.HmacSha256Signature
                    )
            };

            var token = _tokenHandler.CreateToken(tokenDescriptor);
            return _tokenHandler.WriteToken(token);
        }

        public string GetUserInfoByToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            var jwtToken = _tokenHandler.ReadToken(token.Replace("\"", string.Empty)) as JwtSecurityToken;

            var claim = jwtToken?.Claims.FirstOrDefault(x => x.Type == "unique_name");

            return claim?.Value;
        }

        public bool VerifyToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;

            SecurityToken securityToken;

            try
            {
                _tokenHandler.ValidateToken(
                token.Replace("\"", string.Empty),
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_secrectKey),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                },
                out securityToken);
            }
            catch (SecurityTokenException)
            {
                return false;
            }

            return securityToken != null;
        }
    }
}