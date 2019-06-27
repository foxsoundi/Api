using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Spotify.Connections;

namespace Api.Modules
{
    public class TrackModule: NancyModule
    {
        public TrackModule(SpotifyTrackConnection spotifyTrackConnection) : base("v1/music/track")
        {
            Get("/{id}", async parameter => await spotifyTrackConnection.GetTrack(parameter.id));
            Get("/", async _ => await spotifyTrackConnection.GetTracks());
        }
    }
}
