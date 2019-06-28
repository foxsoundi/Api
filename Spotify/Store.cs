using System.Collections.Generic;
using Shared;

namespace Spotify
{
    public class Store
    {
        private List<Profil> LoggedUser { get; } = new List<Profil>();

        public Profil LogNewUser(LoginDto logDto)
        {
            Profil user = new Profil(logDto);
            LoggedUser.Add(user);
            return user;
        }
    }
}