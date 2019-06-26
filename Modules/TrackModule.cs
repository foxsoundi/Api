using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;

namespace Api.Modules
{
    public class TrackModule: NancyModule
    {
        public TrackModule() : base("v1/music/track")
        {
            Get("/{id}", async parameter => await HomeModule.connection.TrackConnection.GetTrack(parameter.id));
            Get("/", async _ => await HomeModule.connection.TrackConnection.GetTracks());
        }
    }
}
