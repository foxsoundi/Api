using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Shared;
using Spotify;
using Spotify.Connections;

namespace Api.Modules
{
    public class PlayerModule : NancyModule
    {
        public PlayerModule(SpotifyConnection spotifyConnection, Store store) : base("v1/player")
        {
            Get("token", _ => spotifyConnection.GetCurrentToken());
            Post("login", parameters =>
            {
                CredentialDto credentialDto = this.Bind<CredentialDto>();
                return Response.AsJson(spotifyConnection.Login(store, credentialDto));
            });
            Get("info/{token}", parameters =>
            {
                Guid sessionToken = Guid.Parse(parameters.token);
                return store.GetProfilOf(sessionToken);
            });
        }
    }
}
