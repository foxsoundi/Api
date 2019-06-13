using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class MusicModule : NancyModule
    {
        public MusicModule(INancyEnvironment environnement) : base("v1/music")
        {
            Get("playlists/{genre}", async parameter => await HomeModule.connection.GetPlaylist(parameter.genre));
            Get("genres", async _ => await HomeModule.connection.GetGenres());
            Get("track/{id}", async parameter => await HomeModule.connection.GetTrack(parameter.id));
        }
    }
}
