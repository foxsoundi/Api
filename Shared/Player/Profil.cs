using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Shared.Player
{
    public class Profil : IToDto<LoginDto>
    {
        private int id;
        public Guid SessionId { get; }
        public string Email { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public Profil(Database.Player player)
        {
            this.id = player.Id;
            this.Email = player.Email;
            this.FirstName = player.FirstName;
            this.LastName = player.LastName;
            this.SessionId = Guid.NewGuid();
        }

        public LoginDto GetDto() => new LoginDto {Profil = this};

        public Database.Player Find(DbSet<Database.Player> players) => players.Find(id);
    }
}
