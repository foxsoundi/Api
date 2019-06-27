using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Spotify.Connections;

namespace Api.Modules
{
    public class PlayerModule : NancyModule
    {
        public PlayerModule(SpotifyConnection spotifyConnection) : base("v1/player")
        {
            Get("token", _ => spotifyConnection.GetCurrentToken());
        }
    }
}
