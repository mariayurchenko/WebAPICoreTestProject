using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Auth;

namespace WebApi.Controllers.Auth
{
    [ApiController]
    public class AuthController: ControllerBase
    {

        private readonly ICustomUserManager _customUserManager;
        private readonly ICustomTokenManager _customTokenManager;

        public AuthController(ICustomUserManager customUserManager,
            ICustomTokenManager customTokenManager)
        {
            _customUserManager = customUserManager;
            _customTokenManager = customTokenManager;
        }

        [HttpPost]
        [Route("/authenticate")]
        public Task<string> Authenticate(UserCredential userCredential)
        {
            return Task.FromResult(_customUserManager.Authenticate(userCredential.userName, userCredential.password));
        }

        [HttpPost]
        [Route("/verifytoken")]
        public Task<bool> Verify(Token token)
        {
            return Task.FromResult(_customTokenManager.VerifyToken(token.token));
        }

        [HttpPost]
        [Route("/getuserinfo")]
        public Task<string> GetUserInfoByToken(Token token)
        {
            return Task.FromResult(_customTokenManager.GetUserInfoByToken(token.token));
        }
    }

    public class UserCredential
    {
        public string userName { get; set; }
        public string password { get; set; }
    }

    public class Token
    {
        public string token { get; set; }
    }
}