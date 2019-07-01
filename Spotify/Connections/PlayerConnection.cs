using Shared;
using Shared.Player;

namespace Spotify.Connections
{
    public class PlayerConnection
    {
        private readonly PlayerStore playerStore;

        public PlayerConnection(PlayerStore playerStore)
        {
            this.playerStore = playerStore;
        }

        public LoginDto Login(CredentialDto credentialDto)
        {
            LogIn state = playerStore.LogNewUser(credentialDto);
            if (state == LogIn.Failed)
                return new LoginDto{IsLoggedIn = LogIn.Failed, Profil = null};

            LoginDto dto = playerStore.GetProfilOf(credentialDto).GetDto();
            dto.IsLoggedIn = state;
            return dto;
        }

        public SignUp SignUp(SignUpDto signUpDto) => playerStore.SignUpUser(signUpDto);
    }
}