using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace App.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IJSRuntime _iJsRuntime;

        public TokenRepository(IJSRuntime iJsRuntime)
        {
            this._iJsRuntime = iJsRuntime;
        }

        public async Task SetToken(string token)
        {
            await _iJsRuntime.InvokeVoidAsync("sessionStorage.setItem", "token", token);
        }

        public async Task<string> GetToken()
        {
            return await _iJsRuntime.InvokeAsync<string>("sessionStorage.getItem", "token");
        }
    }
}