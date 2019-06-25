using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.Routing;

namespace Api.Modules
{
    public class AlbumModule : NancyModule
    {
        public AlbumModule() : base("v1/music/album")
        {
            Get("", async _ => await HomeController.connection.AlbumConnection.GetAlbums());
            Get("{albumId}", async parameters => await HomeController.connection.AlbumConnection.GetAlbum(parameters.albumId));
            Get("{albumId}/tracks", async parameters => await HomeController.connection.AlbumConnection.GetAlbumTracks(parameters.albumId));
        }
    }
}
