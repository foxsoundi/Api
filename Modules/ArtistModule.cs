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
        public ArtistModule(SpotifyArtistConnection spotifyArtistConnection) : base("v1/music/artist")
        {
            Get("", async _ => await spotifyArtistConnection.GetArtists());
            Get("{artistId}", async parameters => await spotifyArtistConnection.GetArtist(parameters.artistId));
            Get("{artistId}/albums", async parameters => await spotifyArtistConnection.GetArtistAlbums(parameters.artistId));
            Get("{artistId}/tops", async parameters => await spotifyArtistConnection.GetArtistTops(parameters.artistId));
            Get("{artistId}/related-artists", async parameters => await spotifyArtistConnection.GetArtistRelateds(parameters.artistId));
        }
    }
}
