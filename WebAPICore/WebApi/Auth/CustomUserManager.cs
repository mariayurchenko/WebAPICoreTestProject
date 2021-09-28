using System.Collections.Generic;

namespace WebApi.Auth
{
    public class CustomUserManager : ICustomUserManager
    {
        private readonly Dictionary<string, string> _credentials = new()
        {
            { "frank", "password" },
            { "tom", "password1" }
        };

        private readonly ICustomTokenManager _customTokenManager;

        public CustomUserManager(ICustomTokenManager customTokenManager)
        {
            this._customTokenManager = customTokenManager;
        }

        public string Authenticate(string userName, string password)
        {
            //validate the credentials              
            if (string.IsNullOrWhiteSpace(userName) || _credentials.GetValueOrDefault(userName) != password) return string.Empty;

            //generate token
            return _customTokenManager.CreateToken(userName);
        }

    }
}