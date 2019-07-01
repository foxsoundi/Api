using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class Playlist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Image FrontImage { get; set; }
        public List<Track> Tracks { get; set; }
        public Player CreatedBy { get; set; }
        public List<PlayerFavouritePlaylist> FavouriteBy { get; set; }
    }
}
