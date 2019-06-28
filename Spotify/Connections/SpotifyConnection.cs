﻿using System;
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
        private Access access;
        private ISecret spotifySecret;

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
                    client.DefaultRequestHeaders.Authorization = spotifySecret.GetBasicAuthenticationHeaderValue();
                    await Connect();
                });
                client.DefaultRequestHeaders.Authorization = access.GetAuthentication();
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
