using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main()
        {
            System.Threading.Thread.Sleep(5000);

            var client = new HttpClient();
            var disc = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

            if (disc.IsError)
            {
                Console.WriteLine(disc.Error);
                return;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disc.TokenEndpoint,

                ClientId = "console.client",
                ClientSecret = "secret",
                Scope = "webapi write"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.ReadLine();
        }
    }
}