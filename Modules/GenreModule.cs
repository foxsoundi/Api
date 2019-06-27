using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                Response rep = await genreConnection.GetGenres();
                rep.ContentType = "application/json";
                return rep;
                //return await HomeModule.connection.GenreConnection.GetGenres();
            });
        }
    }
}
