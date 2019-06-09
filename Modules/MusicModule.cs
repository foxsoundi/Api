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
        private IDatabase database = new MyInMemoryDatabase();
        public MusicModule(INancyEnvironment environnement) : base("v1/music")
        {
            Get("genres", _ => Response.AsJson(database.GetGenres().Select(
                                    g => 
                                    {
                                        g.Image = "data:image/jpeg;base64," + g.Image;
                                        return g;
                                    })
                               ));
        }
    }
}