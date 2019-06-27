using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Spotify.Connections;

namespace Api.Modules
{
    public class ArtistModule : NancyModule
    {
        public ArtistModule(ArtistConnection artistConnection) : base("v1/music/artist")
        {
            Get("", async _ => await artistConnection.GetArtists());
            Get("{artistId}", async parameters => await artistConnection.GetArtist(parameters.artistId));
            Get("{artistId}/albums", async parameters => await artistConnection.GetArtistAlbums(parameters.artistId));
            Get("{artistId}/tops", async parameters => await artistConnection.GetArtistTops(parameters.artistId));
            Get("{artistId}/related-artists", async parameters => await artistConnection.GetArtistRelateds(parameters.artistId));
        }
    }
}
