using System;
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

        public Profil GetInfoOf(Guid sessionToken)
        {
            return LoggedUser.Find(p => p.SessionId == sessionToken);
        }
    }
}