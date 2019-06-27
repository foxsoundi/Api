﻿using Api.Spotify;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Configuration;
using Nancy.IO;
using Nancy.ModelBinding;
using System;
using System.Net.Http;
using Spotify.Connections;

namespace Api
{
    public class HomeModule : NancyModule
    {
        //public static SpotifyConnection connection;
        public HomeModule(INancyEnvironment environnement, SpotifyConnection connection)
        {
            Get("/", _ => "Hello World!");
            Get("/ping", async _ => await connection.Ping());
            Post("/connect", async _ =>
            {
                MySecrets secret = this.Bind<MySecrets>();
                connection.AddAndUseSecret(secret);
                //HomeModule.connection = connection;
                return await connection.Connect();
            });
        }
    }
}
