using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Shared
{
    public class PlaylistStore
    {
        private readonly FoxsoundiContext dbContext;
        private readonly PlayerStore playerStore;

        public PlaylistStore(FoxsoundiContext dbContext, PlayerStore playerStore)
        {
            this.dbContext = dbContext;
            this.playerStore = playerStore;
        }
        public async Task<PlaylistDto> CreateNewPlaylist(PlaylistDto dto, Guid sessionId)
        {
            Database.Player currentPlayer = playerStore.GetLoggedPlayer(sessionId);
            if(currentPlayer.PersonnalPlaylists == null)
                currentPlayer.PersonnalPlaylists = new List<Playlist>();
            if(currentPlayer.FavouritePlaylists == null)
                currentPlayer.FavouritePlaylists = new List<PlayerFavouritePlaylist>();

            currentPlayer.PersonnalPlaylists.Add(new Database.Playlist
            {
                CreatedBy = currentPlayer,
                Description = dto.description,
                Name = dto.name
            });
            dto.owner = new Owner {display_name = $"{currentPlayer.LastName + currentPlayer.FirstName}"};
            dbContext.Update(currentPlayer);
            await dbContext.SaveChangesAsync();
            playerStore.Refresh(currentPlayer);
            return dto;
        }
    }
}
