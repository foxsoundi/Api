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
        public PlaylistModule(SpotifyPlaylistConnection spotifyPlaylistConnection) : base("v1/music/playlist")
        {
            Get("{playListId}", async parameter => await spotifyPlaylistConnection.GetPlaylist(parameter.playListId));
            Get("{playListId}/tracks", async parameter => await spotifyPlaylistConnection.GetPlaylistTracks(parameter.playListId));
        }
    }
}
