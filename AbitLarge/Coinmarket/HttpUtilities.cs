using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoinMarketCap.Helpers
{
    public static class HttpUtilities
    {
        public static async Task<string> GetHttpResponseAsync(string url)
        {
            var client = new HttpClient();

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await client.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var message = $"Request failed with HTTP status {response.StatusCode} ({response.ReasonPhrase})\n{responseContent}";

                throw new Exception($"{(int)response.StatusCode} {message}");
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}
