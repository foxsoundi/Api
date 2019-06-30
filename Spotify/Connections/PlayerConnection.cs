using Shared;
using Shared.Player;

namespace Spotify.Connections
{
    public class PlayerConnection
    {
        private readonly Store store;

        public PlayerConnection(Store store)
        {
            this.store = store;
        }

        public LoginDto Login(CredentialDto credentialDto)
        {
            LogIn state = store.LogNewUser(credentialDto);
            if (state == LogIn.Failed)
                return new LoginDto{IsLoggedIn = LogIn.Failed, Profil = null};

            LoginDto dto = store.GetProfilOf(credentialDto).GetDto();
            dto.IsLoggedIn = state;
            return dto;
        }

        public SignUp SignUp(SignUpDto signUpDto) => store.SignUpUser(signUpDto);
    }
}