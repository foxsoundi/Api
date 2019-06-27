using Api.Spotify;
using Nancy;
using Spotify.Connections;

namespace Api.Modules
{
    public class GenreModule : NancyModule
    {
        public GenreModule(GenreConnection genreConnection) : base("v1/music/genre")
        {
            Get("{genreId}", async parameters => await genreConnection.GetGenres(parameters.genreId));
            Get("/", async parameters =>
            {
                GenreDto genresdto = await genreConnection.GetGenres();
                Response rep = Response.AsJson(genresdto);
                rep.ContentType = "application/json";
                return rep;
            });
        }
    }
}
