using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Shared.Player;

namespace Shared
{
    public class PlayerStore
    {
        private readonly FoxsoundiContext dbContext;
        private List<Profil> LoggedUsers { get; } = new List<Profil>();
        private readonly Func<List<Credential>> AllUsersCredential;

        public PlayerStore(FoxsoundiContext context)
        {
            this.dbContext = context;
            AllUsersCredential = () => dbContext.Players.Select(p => new Credential(p)).ToList();
        }

        public LogIn LogNewUser(CredentialDto logDto)
        {
            if (!AllUsersCredential().Any(p => p.CheckCredential(logDto)))
                return LogIn.Failed;
            //Credential hisCred = AllUsersCredential.Find(c => c.isTheSameThan(logDto));
            Database.Player userDb = dbContext.Players.FirstOrDefault(p => p.Email == logDto.Email);
            Profil user = new Profil(userDb);
            LoggedUsers.Add(user);
            return LogIn.Success;
        }

        public Profil GetProfilOf(Guid sessionToken) => LoggedUsers.Find(p => p.SessionId == sessionToken);

        public Profil GetProfilOf(CredentialDto credentialDto) => LoggedUsers.Find(p => p.Email == credentialDto.Email);

        public SignUp SignUpUser(SignUpDto signUpDto)
        {
            if (AllUsersCredential().Any(c => c.CheckCredential(signUpDto)))
                return SignUp.Exist;

            dbContext.Players.Add(new Database.Player
            {
                Email = signUpDto.Email,
                Password = signUpDto.Password,
                LastName = signUpDto.LastName,
                FirstName = signUpDto.FirstName
            });

            dbContext.SaveChanges();
            return SignUp.New;
        }

        public Database.Player GetLoggedPlayer(Guid sessionId)
        {
            Profil loggedProfil = LoggedUsers.Single(p => p.SessionId == sessionId);
            return loggedProfil.Find(dbContext.Players);
        }

        public void Refresh(Database.Player currentPlayer)
        {
            LoggedUsers.Remove(LoggedUsers.Find(x => x.Email == currentPlayer.Email));
            LoggedUsers.Add(new Profil(currentPlayer));
        }
    }
}