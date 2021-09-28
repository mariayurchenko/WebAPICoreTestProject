using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace App.Repository.ApiClient
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;

        public WebApiExecuter(HttpClient httpClient)
        {
            _baseUrl = httpClient.BaseAddress.AbsoluteUri;
            _httpClient = httpClient;
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> InvokeGet<T>(string uri)
        {
            //await AddTokenHeader();
            return await _httpClient.GetFromJsonAsync<T>(GetUrl(uri));
        }

        public async Task<T> InvokePost<T>(string uri, T obj)
        {
            //await AddTokenHeader();
            var response = await _httpClient.PostAsJsonAsync(GetUrl(uri), obj);
            await HandleError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<string> InvokePostReturnString<T>(string uri, T obj)
        {
            //await AddTokenHeader();
            var response = await _httpClient.PostAsJsonAsync(GetUrl(uri), obj);
            await HandleError(response);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task InvokePut<T>(string uri, T obj)
        {
            //await AddTokenHeader();
            var response = await _httpClient.PutAsJsonAsync(GetUrl(uri), obj);
            await HandleError(response);
        }

        public async Task InvokeDelete(string uri)
        {
            //await AddTokenHeader();
            var response = await _httpClient.DeleteAsync(GetUrl(uri));
            await HandleError(response);
        }

        private string GetUrl(string uri)
        {
            return _baseUrl.EndsWith("/") ? $"{_baseUrl}{uri}" : $"{_baseUrl}/{uri}";
        }

        private async Task HandleError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error);
            }
        }

       /* private async Task AddTokenHeader()
        {
            if (_tokenRepository != null && !string.IsNullOrWhiteSpace(await _tokenRepository.GetToken()))
            {
                _httpClient.DefaultRequestHeaders.Remove("TokenHeader");
                _httpClient.DefaultRequestHeaders.Add("TokenHeader", await _tokenRepository.GetToken());
            }
        }*/

    }
}