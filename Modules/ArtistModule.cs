using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;

namespace Api.Modules
{
    public class ArtistModule : NancyModule
    {
        public ArtistModule() : base("v1/music/artist")
        {
            Get("", async _ => await HomeModule.connection.ArtistConnection.GetArtists());
            Get("{artistId}", async parameters => await HomeModule.connection.ArtistConnection.GetArtist(parameters.artistId));
            Get("{artistId}/albums", async parameters => await HomeModule.connection.ArtistConnection.GetArtistAlbums(parameters.artistId));
            Get("{artistId}/tops", async parameters => await HomeModule.connection.ArtistConnection.GetArtistTops(parameters.artistId));
            Get("{artistId}/related-artists", async parameters => await HomeModule.connection.ArtistConnection.GetArtistRelateds(parameters.artistId));
        }
    }
}
