using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;

namespace Api.Modules
{
    public class GenreModule : NancyModule
    {
        public GenreModule() : base("v1/music/genre")
        {
            Get("{genreId}", async parameters => await HomeController.connection.GenreConnection.GetGenres(parameters.genreId));
            Get("", async parameters => await HomeController.connection.GenreConnection.GetGenres(parameters.genreId));
        }
    }
}
