using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;

namespace Api.Modules
{
    public class PlayerModule : NancyModule
    {
        public PlayerModule() : base("v1/player")
        {
            Get("token", _ => HomeModule.connection.GetCurrentToken());
        }
    }
}
