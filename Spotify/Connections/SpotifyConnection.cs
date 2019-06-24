using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Api;
using Api.Spotify;
using Newtonsoft.Json;

namespace Spotify.Connections
{
    public class SpotifyConnection
    {
        private readonly HttpClient client;

        private Access access;
        private readonly MySecrets secret;

        public SpotifyConnection(MySecrets secret, HttpClient client)
        {
            this.secret = secret;
            var scopes = "user-read-private user-read-email";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
                    Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{secret.Id}:{secret.Secret}")));
            this.client = client;
            AlbumConnection = new AlbumConnection(ref this.client);
            ArtistConnection = new ArtistConnection(ref this.client);
            PlaylistConnection = new PlaylistConnection(ref this.client);
            GenreConnection = new GenreConnection(ref this.client);
            TrackConnection = new TrackConnection(ref this.client);
        }

        public AlbumConnection AlbumConnection { get; }
        public ArtistConnection ArtistConnection { get; }
        public PlaylistConnection PlaylistConnection { get; }
        public GenreConnection GenreConnection { get; }
        public TrackConnection TrackConnection { get; }

        public async Task<HttpStatusCode> Connect()
        {
            Dictionary<string, string> payload = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials"}
            };

            Uri url = new Uri("https://accounts.spotify.com/api/token/");
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(payload)
            };
            try
            {
                HttpResponseMessage res = await client.SendAsync(req);
                if (!res.IsSuccessStatusCode)
                    return res.StatusCode;

                string content = await res.Content.ReadAsStringAsync();
                AccessDto accessDto = JsonConvert.DeserializeObject<AccessDto>(content);
                access = new Access(accessDto, async () =>
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{secret.Id}:{secret.Secret}")));
                    await Connect();
                });
                client.DefaultRequestHeaders.Authorization = await access.GetAuthentication();
            }
            catch (Exception e)
            {
                throw e;
            }
            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> Ping()
        {
            string spotIdTest = "6ZEYvUSgON3J5Qe1RYi3Jo";
            Uri trackUrl = new Uri($"https://api.spotify.com/v1/tracks/{spotIdTest}");
            HttpResponseMessage response = await client.GetAsync(trackUrl);

            return response.StatusCode;
        }

        public string GetCurrentToken()
        {
            return access.Token;
        }
    }
}
