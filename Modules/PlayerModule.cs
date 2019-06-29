using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;
using Shared;
using Shared.Player;
using Spotify;
using Spotify.Connections;

namespace Api.Modules
{
    public class PlayerModule : NancyModule
    {
        public PlayerModule(SpotifyConnection spotifyConnection, Store store, PlayerConnection playerConnection) : base("v1/player")
        {
            Get("token", _ => spotifyConnection.GetCurrentToken());
            Post("login", parameters =>
            {
                CredentialDto credentialDto = this.Bind<CredentialDto>();
                return Response.AsJson(playerConnection.Login(credentialDto));
            });

            Post("signUp", parameters =>
            {
                SignUpDto signUpDto = this.Bind<SignUpDto>();
                return Response.AsJson(playerConnection.SignUp(signUpDto));
            });
            Get("info/{token}", parameters =>
            {
                Guid sessionToken = Guid.Parse(parameters.token);
                return store.GetProfilOf(sessionToken);
            });
        }
    }
}
