using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class Player
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public UsageLevel IsAdmin { get; set; }
        public List<Player> Friends { get; set; }
        public List<Track> FavouriteTracks { get; set; }
        public List<Playlist> FavouritePlaylists { get; set; }
        public Preference Preferences { get; set; }
    }
}
