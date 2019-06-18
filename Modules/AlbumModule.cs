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
        public AlbumModule() : base("v1/music/albums")
        {
            Get("{albumId}", async parameters => HomeModule.connection.GetAlbums(parameters.albumId));
        }
    }
}
