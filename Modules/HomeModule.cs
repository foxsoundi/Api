using Api.Spotify;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Configuration;
using Nancy.IO;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Napster;
using Newtonsoft.Json;
using Shared;
using Spotify.Connections;

namespace Api
{
    public class HomeModule : NancyModule
    {
        public HomeModule(INancyEnvironment environnement, SpotifyConnection spotifyConnection,
            NapsterConnection napsterConnection)
        {
            Get("/", _ => "Hello World!");
            Get("/ping", _ => HttpStatusCode.Accepted);
            Post("/connect", async _ =>
            {
                SpotifySecrets spotifySecret = this.Bind<SpotifySecrets>();
                NapsterSecrets napsterSecrets = this.Bind<NapsterSecrets>();
                spotifyConnection.AddAndUseSecret(spotifySecret);
                return await spotifyConnection.Connect();
            });
        }
    }
}

   
