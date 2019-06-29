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
using Shared.Player;

namespace Spotify.Connections
{
    public class PlayerConnection
    {
        private Store store;

        public PlayerConnection(Store store)
        {
            this.store = store;
        }

        public LoginDto Login(CredentialDto credentialDto)
        {
            LogIn state = store.LogNewUser(credentialDto);
            if (state == LogIn.Failed)
                return new LoginDto{IsLoggedIn = LogIn.Failed, Profil = null};

            LoginDto dto = store.GetProfilOf(credentialDto).GetDto();
            dto.IsLoggedIn = state;
            return dto;
        }

        public SignUp SignUp(SignUpDto signUpDto) => store.SignUpUser(signUpDto);
    }

    public class SpotifyConnection
    {
        private readonly HttpClient client;
        private Access access = new Access();
        private ISecret spotifySecret;
        private readonly PlayerConnection playerConnection;

        public SpotifyConnection(HttpClient client)
        {
            // var scopes = "user-read-private user-read-email";
            this.client = client;
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
            access = new Access(accessDto, async () =>
            {
                client.DefaultRequestHeaders.Authorization = spotifySecret.GetBasicAuthenticationHeaderValue();
                await Connect();
                access.IsConnected = true;
            });
            client.DefaultRequestHeaders.Authorization = access.GetAuthentication();

            return HttpStatusCode.OK;
        }

        public bool Ping() => access.IsConnected;

        public string GetCurrentToken() => access.Token;
    }
}