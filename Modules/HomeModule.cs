using Api.Spotify;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Configuration;
using Nancy.IO;
using Nancy.ModelBinding;
using System;
using System.Net.Http;
using Spotify.Connections;

namespace Api
{
    public class HomeModule : NancyModule
    {
        public HomeModule(INancyEnvironment environnement, SpotifyConnection spotifyConnection)
        {
            Get("/", _ => "Hello World!");
            Get("/ping", async _ => await spotifyConnection.Ping());
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
