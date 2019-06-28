using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Napster
{
    public class NapsterConnection
    {
        private readonly HttpClient client;
        private ISecret napsterSecret;

        public NapsterConnection(HttpClient client)
        {
            // var scopes = "user-read-private user-read-email";

            this.client = client;
        }

        public void AddAndUseSecret(ISecret napsterSecrets)
        {
            this.napsterSecret = napsterSecrets;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = napsterSecret.GetBasicAuthenticationHeaderValue();
        }

        public async Task<HttpStatusCode> Connect()
        {
            //Dictionary<string, string> payload = new Dictionary<string, string>
            //{
            //    { "grant_type", "client_credentials"}
            //};

            //Uri url = new Uri("https://accounts.spotify.com/api/token/");
            //HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, url)
            //{
            //    Content = new FormUrlEncodedContent(payload)
            //};
            //try
            //{
            //    HttpResponseMessage res = await client.SendAsync(req);
            //    if (!res.IsSuccessStatusCode)
            //        return res.StatusCode;

            //    string content = await res.Content.ReadAsStringAsync();
            //    AccessDto accessDto = JsonConvert.DeserializeObject<AccessDto>(content);
            //    access = new Access(accessDto, async () =>
            //    {
            //        client.DefaultRequestHeaders.Authorization = spotifySecret.GetBasicAuthenticationHeaderValue();
            //        await Connect();
            //    });
            //    client.DefaultRequestHeaders.Authorization = access.GetAuthentication();
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            return HttpStatusCode.OK;
        }
    }
}
