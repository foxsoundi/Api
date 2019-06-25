using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;

namespace Api.Modules
{
    public class TrackModule: NancyModule
    {
        //public TrackModule() : base("v1/music/track")
        //{
        //    Get("track/{id}", async parameter => await HomeController.connection.TrackConnection.GetTrack(parameter.id));
        //    Get("track", async _ => await HomeController.connection.TrackConnection.GetTracks());
        //}
    }
}
