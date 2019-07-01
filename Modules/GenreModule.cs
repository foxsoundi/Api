using Api.Spotify;
using Nancy;
using Spotify.Connections;

namespace Api.Modules
{
    public class GenreModule : NancyModule
    {
        public GenreModule(SpotifyGenreConnection spotifyGenreConnection) : base("v1/music/genre")
        {
            Get("{genreId}", async parameters => await spotifyGenreConnection.GetGenres(parameters.genreId));
            Get("{genreId}/playlists", async parameters => await spotifyGenreConnection.GetGenrePlaylist(parameters.genreId));
            Get("/", async parameters =>
            {
                GenresDto genresdto = await spotifyGenreConnection.GetGenres();
                Response rep = Response.AsJson(genresdto);
                rep.ContentType = "application/json";
                return rep;
            });
        }
    }
}
