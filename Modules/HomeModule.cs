using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Configuration;
using Api.Spotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class HomeModule : NancyModule
    {
        SpotifyConnection connection;
        public HomeModule(INancyEnvironment environnement)
        {
            var secrets = environnement.GetValue<MySecrets>();
            connection = new SpotifyConnection(secrets);

            Get("/", _ => "Hello World!");
            Get("/ping", async _ => await connection.Ping());
            Get("/connect", async _ =>
            {
                
                return await connection.Connect();
            });
        }
    }
}
