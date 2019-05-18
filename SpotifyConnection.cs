using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Api
{
    public class SpotifyConnection
    {
        private HttpClient client = new HttpClient();

        public SpotifyConnection(MySecrets secret)
        {
            var scopes = "user-read-private user-read-email";

            client.BaseAddress = new Uri("https://api.spotify.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        async public Task<string> Ping()
        {
            HttpResponseMessage response = await client.GetAsync("v1/albums/1");
            if (response.IsSuccessStatusCode)
            {
                return "Success";
            }
            return "Failed";
        }
    }
}
