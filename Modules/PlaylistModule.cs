using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nancy;
using Nancy.Configuration;

namespace Api.Modules
{
    [Route("v1/music/playlist")]
    [Produces("application/json")]
    [ApiController]
    public class PlaylistModule : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> GetPlaylist(string playlistId)
        {
            return await HomeController.connection.PlaylistConnection.GetPlaylist(playlistId);
        }
    }
}
