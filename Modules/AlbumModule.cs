using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.Routing;
using Spotify.Connections;

namespace Api.Modules
{
    public class AlbumModule : NancyModule
    {
        public AlbumModule(SpotifyAlbumConnection spotifyAlbumConnection) : base("v1/music/album")
        {
            Get("", async _ => await spotifyAlbumConnection.GetAlbums(new string[2]));
            Get("{albumId}", async parameters => await spotifyAlbumConnection.GetAlbum(parameters.albumId));
            Get("{albumId}/tracks", async parameters => await spotifyAlbumConnection.GetAlbumTracks(parameters.albumId));
        }
    }
}
