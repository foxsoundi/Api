using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared
{
    public class Store
    {
        private List<Profil> LoggedUsers { get; } = new List<Profil>();
        private List<Credential> AllUsersCredential { get; } = new List<Credential>();

        public LogIn LogNewUser(CredentialDto logDto)
        {
            if (!AllUsersCredential.Any(p => p.isTheSameThan(logDto)))
                return LogIn.Failed;

            Profil user = new Profil(AllUsersCredential.Find(c => c.isTheSameThan(logDto)).GetDto());
            LoggedUsers.Add(user);
            return LogIn.Success;
        }

        public Profil GetProfilOf(Guid sessionToken)
        {
            return LoggedUsers.Find(p => p.SessionId == sessionToken);
        }

        public Profil GetProfilOf(CredentialDto credentialDto)
        {
            return LoggedUsers.Find(p => p.email == credentialDto.Email);
        }

        public SignUp SignUpUser(SignUpDto signUpDto)
        {
            if(AllUsersCredential.Any(c => c.isTheSameThan(signUpDto)))
                return SignUp.Exist;

            AllUsersCredential.Add(new Credential(signUpDto));
            return SignUp.New;
        }
    }
}