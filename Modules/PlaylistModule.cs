using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.Configuration;
using Spotify.Connections;

namespace Api.Modules
{
    public class PlaylistModule : NancyModule
    {
        public PlaylistModule(PlaylistConnection playlistConnection) : base("v1/music/playlist")
        {
            Get("{playListId}", async parameter => await playlistConnection.GetPlaylist(parameter.playListId));
        }
    }
}
