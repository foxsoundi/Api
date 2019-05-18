using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class HomeModule : NancyModule
    {
        public HomeModule(INancyEnvironment environnement)
        {
            Get("/", _ => "Hello World!");
            Get("/ping", async _ =>
            {
                var secrets = environnement.GetValue<MySecrets>();
                SpotifyConnection connection = new SpotifyConnection(secrets);
                var result = await connection.Ping();
                return result;
            });
        }
    }
}
