using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Api.Spotify
{
    public class SpotifyConnection
    {
        private readonly HttpClient client = new HttpClient();
        private event EventHandler triggerReconnect;
        private Access access;

        public SpotifyConnection(MySecrets secret)
        {
            var scopes = "user-read-private user-read-email";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
                    Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{secret.Id}:{secret.Secret}")));
            triggerReconnect += async (t, e) => await Connect();
        }

        public async Task<string> GetGenres()
        {
            Uri GenresUrl = new Uri($"https://api.spotify.com/v1/browse/categories");
            HttpResponseMessage response = await client.GetAsync(GenresUrl);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpStatusCode> Connect()
        {
            Dictionary<string, string> payload = new Dictionary<string, string>();
            payload.Add("grant_type", "client_credentials");

            Uri url = new Uri("https://accounts.spotify.com/api/token/");
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent (payload)
            };

            HttpResponseMessage res = await client.SendAsync(req);
            if (!res.IsSuccessStatusCode)
                return res.StatusCode;

            string content = await res.Content.ReadAsStringAsync();
            AccessDto accessDto = JsonConvert.DeserializeObject<AccessDto>(content);
            access = new Access(accessDto);
            Task refreshTask = Task.Run(async () => await ReconnectIn(access.ExpireIn));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(access.Type, access.Token);
            return HttpStatusCode.OK;
        }

        private async Task ReconnectIn(TimeSpan timespan)
        {
            await Task.Delay(timespan - TimeSpan.FromSeconds(10));
            triggerReconnect?.Invoke(this, null);
        }

        public async Task<string> GetTrack(string id)
        {
            Uri trackUrl = new Uri($"https://api.spotify.com/v1/tracks/{id}?market=FR");
            HttpResponseMessage response = await client.GetAsync(trackUrl);
            string content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task<string> GetPlaylist(string genreId)
        {
            Uri playlistUrl = new Uri($"https://api.spotify.com/v1/browse/categories/{genreId}");
            HttpResponseMessage response = await client.GetAsync(playlistUrl);
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }

        public async Task<HttpStatusCode> Ping()
        {
            string spotIdTest = "6ZEYvUSgON3J5Qe1RYi3Jo";
            Uri trackUrl = new Uri($"https://api.spotify.com/v1/tracks/{spotIdTest}");
            HttpResponseMessage response = await client.GetAsync(trackUrl);

            return response.StatusCode;
        }
    }
}
