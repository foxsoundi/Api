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
using Youtube;

namespace Api
{
    public class HomeModule : NancyModule
    {
        public HomeModule(SpotifyConnection spotifyConnection,
                            NapsterConnection napsterConnection,
                            YoutubeConnection youtubeConnection)
        {
            Get("/", _ => "Hello World!");
            Get("/ping", _ =>
            {
                if (spotifyConnection.Ping()) return true;
                return false;
            });
            Post("/connect", async _ =>
            {
                SpotifySecrets spotifySecret = this.Bind<SpotifySecrets>();
                NapsterSecrets napsterSecrets = this.Bind<NapsterSecrets>();
                YoutubeSecrets youtubeSecrets = this.Bind<YoutubeSecrets>();
                spotifyConnection.AddAndUseSecret(spotifySecret);
                youtubeConnection.AddAndUseSecret(youtubeSecrets);
                return await spotifyConnection.Connect();
            });
        }
    }
}

   
