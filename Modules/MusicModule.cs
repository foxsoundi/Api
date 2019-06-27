using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spotify.Connections;

namespace Api
{
    public class MusicModule : NancyModule
    {
        public MusicModule(SpotifyTrackConnection spotifyTrackConnection) : base("v1/music")
        {
             Get("audio-feature", async _ => await spotifyTrackConnection.GetAudioFeature());
        }
    }
}
