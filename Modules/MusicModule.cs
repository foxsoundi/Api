﻿using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class MusicModule : NancyModule
    {
        public MusicModule(INancyEnvironment environnement) : base("v1/music")
        {
            Get("track/{id}", async parameter => await HomeModule.connection.GetTrack(parameter.id));
        }
    }
}
