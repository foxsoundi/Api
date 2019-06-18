using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.Configuration;

namespace Api.Modules
{
    public class PlaylistModule : NancyModule
    {
        public PlaylistModule(INancyEnvironment environment) : base("v1/music/playlist")
        {
            Get("{genre}", async parameter => await HomeModule.connection.GetPlaylist(parameter.genre));
        }
    }
}
