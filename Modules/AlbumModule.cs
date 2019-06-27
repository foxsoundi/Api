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
        public AlbumModule(AlbumConnection albumConnection) : base("v1/music/album")
        {
            Get("", async _ => await albumConnection.GetAlbums());
            Get("{albumId}", async parameters => await albumConnection.GetAlbum(parameters.albumId));
            Get("{albumId}/tracks", async parameters => await albumConnection.GetAlbumTracks(parameters.albumId));
        }
    }
}
