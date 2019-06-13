using Api.Spotify;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Configuration;
using Nancy.IO;
using Nancy.ModelBinding;
using System;

namespace Api
{
    public class HomeModule : NancyModule
    {
        public static SpotifyConnection connection;
        public HomeModule(INancyEnvironment environnement)
        {
            Get("/", _ => "Hello World!");
            Get("/ping", async _ => await connection.Ping());
            Post("/connect", async _ =>
            {
                MySecrets secret = this.Bind<MySecrets>();
                // var secrets = environnement.GetValue<MySecrets>();
                connection = new SpotifyConnection(secret);
                return await connection.Connect();
            });
        }
    }
}
