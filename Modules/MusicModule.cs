using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared;
using Spotify.Connections;
using Youtube;

namespace Api
{
    public class MusicModule : NancyModule
    {
        public MusicModule(SpotifyTrackConnection spotifyTrackConnection, YoutubeConnection youtubeConnection) : base("v1/music")
        {
            Get("audio-feature", async _ => await spotifyTrackConnection.GetAudioFeature());
            Get("youtube/{songTitle}", async parameters =>
            {
                return Response.AsJson(new YoutubeDto
                {
                    VideoId = await youtubeConnection.GetVideoIdOf(parameters.songTitle)
                });
            });
        }
    }
}
