using Api.Spotify;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nancy.IO;
using Nancy.ModelBinding;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spotify.Connections;

namespace Api
{
    public class HomeController : Controller
    {
        public static SpotifyConnection connection;

        [HttpPost("connect")]
        public async Task<ActionResult<HttpStatusCode>> Connect([FromBody]MySecrets secrets)
        {
            //MySecrets secret = this.Bind<MySecrets>();
            // var secrets = environnement.GetValue<MySecrets>();
            connection = new SpotifyConnection(secrets, new HttpClient());
            return await connection.Connect();
        }
    }
}
