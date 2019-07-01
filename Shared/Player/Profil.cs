using System;
using System.Collections.Generic;
using System.Linq;
using Database;
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

        public List<Playlist> PersonnalPlaylists { get; }
        public Profil(Database.Player player)
        {
            this.id = player.Id;
            this.Email = player.Email;
            this.FirstName = player.FirstName;
            this.LastName = player.LastName;
            this.PersonnalPlaylists = player.PersonnalPlaylists;
            this.FavouritePlaylists = player.FavouritePlaylists?.Where(x => x.PlayerId == id)
                                                                .Select(fp => fp.FavouritePlaylist)
                                                                .ToList();
            this.SessionId = Guid.NewGuid();
        }

        public List<Playlist> FavouritePlaylists { get; set; }

        public LoginDto GetDto() => new LoginDto {Profil = this};

        public Database.Player Find(DbSet<Database.Player> players) => players.Find(id);
    }
}
