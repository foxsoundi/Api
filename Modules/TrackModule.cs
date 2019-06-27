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
        public TrackModule(TrackConnection trackConnection) : base("v1/music/track")
        {
            Get("/{id}", async parameter => await trackConnection.GetTrack(parameter.id));
            Get("/", async _ => await trackConnection.GetTracks());
        }
    }
}
