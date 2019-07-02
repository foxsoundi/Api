using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.Configuration;
using Nancy.ModelBinding;
using Shared;
using Spotify.Connections;

namespace Api.Modules
{
    public class PlaylistModule : NancyModule
    {
        public PlaylistModule(SpotifyPlaylistConnection spotifyPlaylistConnection, PlaylistStore playlistStore) : base("v1/music/playlist")
        {
            Before += ctx =>
            {
                //Request req = ctx.Request.Headers;
                return null;
            };

            Get("{playListId}", async parameter => await spotifyPlaylistConnection.GetPlaylist(parameter.playListId));
            Get("{playListId}/tracks", async parameter => await spotifyPlaylistConnection.GetPlaylistTracks(parameter.playListId));
            Post("/hybrid", async parameter =>
            {
                string sessionId = Request.Headers["SessionId"].ToList()[0];
                PlaylistDto dto = this.Bind<PlaylistDto>();

                return Response.AsJson(await playlistStore.CreateNewPlaylist(dto, Guid.Parse(sessionId)));
            });
            Get("/hybrid/all", async _ =>
            {
                string sessionId = Request.Headers["SessionId"].ToList()[0];
                //PlaylistDto dto = this.Bind<PlaylistDto>();
                return Response.AsJson(await playlistStore.GetAllPlaylist(Guid.Parse(sessionId)));
            });
        }
    }
}
