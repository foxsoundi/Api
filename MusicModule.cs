using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class MusicModule : NancyModule
    {
        public MusicModule(INancyEnvironment environnement)
        {
            Get("/v1/music/genres", _ => "Hey, les genres seront ici très bientôt");
        }
    }
}
