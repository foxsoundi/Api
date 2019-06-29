using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Shared.Player;

namespace Shared
{
    public class Store
    {
        private readonly FoxsoundiContext dbContext;
        private List<Profil> LoggedUsers { get; } = new List<Profil>();
        //private List<Credential> AllUsersCredential { get; } = new List<Credential>
        //{
        //    new Credential(new CredentialDto{Email = "yoyo@gmail.com", Password = "******"}),
        //    new Credential(new CredentialDto{Email = "rahma@gmail.com", Password = "******"}),
        //    new Credential(new CredentialDto{Email = "loghan@gmail.com", Password = "******"})
        //};
        private List<Credential> AllUsersCredential;

        public Store(FoxsoundiContext context)
        {
            this.dbContext = context;
            AllUsersCredential = dbContext.players.Select(p => new Credential(p)).ToList();
        }

        public LogIn LogNewUser(CredentialDto logDto)
        {
            AllUsersCredential = dbContext.players.Select(p => new Credential(p)).ToList();
            if (!AllUsersCredential.Any(p => p.CheckCredential(logDto)))
                return LogIn.Failed;
            //Credential hisCred = AllUsersCredential.Find(c => c.isTheSameThan(logDto));
            Database.Player userDb = dbContext.players.FirstOrDefault(p => p.Email == logDto.Email);
            Profil user = new Profil(userDb);
            LoggedUsers.Add(user);
            return LogIn.Success;
        }

        public Profil GetProfilOf(Guid sessionToken) => LoggedUsers.Find(p => p.SessionId == sessionToken);

        public Profil GetProfilOf(CredentialDto credentialDto) => LoggedUsers.Find(p => p.Email == credentialDto.Email);

        public SignUp SignUpUser(SignUpDto signUpDto)
        {
            if(AllUsersCredential.Any(c => c.CheckCredential(signUpDto)))
                return SignUp.Exist;

            AllUsersCredential.Add(new Credential(signUpDto));
            return SignUp.New;
        }
    }
}