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
        private readonly Func<List<Credential>> AllUsersCredential;

        public Store(FoxsoundiContext context)
        {
            this.dbContext = context;
            AllUsersCredential = () => dbContext.players.Select(p => new Credential(p)).ToList();
        }

        public LogIn LogNewUser(CredentialDto logDto)
        {
            if (!AllUsersCredential().Any(p => p.CheckCredential(logDto)))
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
            if(AllUsersCredential().Any(c => c.CheckCredential(signUpDto)))
                return SignUp.Exist;

            dbContext.players.Add(new Database.Player
            {
                Email = signUpDto.Email,
                Password = signUpDto.Password,
                LastName = signUpDto.LastName,
                FirstName = signUpDto.FirstName
            });

            dbContext.SaveChanges();
            return SignUp.New;
        }
    }
}