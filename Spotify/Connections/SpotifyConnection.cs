using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Api;
using Api.Spotify;
using Newtonsoft.Json;
using Shared;

namespace Spotify.Connections
{
    public class SpotifyConnection
    {
        private readonly HttpClient client;
        private Access access = new Access();
        private ISecret spotifySecret;

        public SpotifyConnection(HttpClient client, Access access)
        {
            this.client = client;
            this.access = access;
        }

        public void AddAndUseSecret(ISecret spotifySecrets)
        {
            this.spotifySecret = spotifySecrets;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = spotifySecrets.GetBasicAuthenticationHeaderValue();
        }

        public async Task<HttpStatusCode> Connect()
        {
            async Task<HttpResponseMessage> GetOauthToken()
            {
                Dictionary<string, string> payload = new Dictionary<string, string>
                {
                    {"grant_type", "client_credentials"}
                };

                Uri url = new Uri("https://accounts.spotify.com/api/token/");
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new FormUrlEncodedContent(payload)
                };
                try
                {
                    HttpResponseMessage res = await client.SendAsync(req);
                    return res;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            HttpResponseMessage oauthResponse = await GetOauthToken();
            string content = await oauthResponse.Content.ReadAsStringAsync();
            AccessDto accessDto = JsonConvert.DeserializeObject<AccessDto>(content);
            access = new Access(accessDto);
            //access = new Access(accessDto, async () =>
            //{
            //    client.DefaultRequestHeaders.Authorization = spotifySecret.GetBasicAuthenticationHeaderValue();
            //    await Connect();
            //    access.IsConnected = true;
            //});
            client.DefaultRequestHeaders.Authorization = access.GetAuthentication();

            return HttpStatusCode.OK;
        }

        public bool Ping() => access.IsConnected;

        public string GetCurrentToken() => access.Token;
    }
}