using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class PlayerFavouritePlaylist
    {
        public Player Player { get; set; }
        public int PlayerId { get; set; }
        public Playlist FavouritePlaylist { get; set; }
        public int FavouritePlaylistId { get; set; }
    }
}
