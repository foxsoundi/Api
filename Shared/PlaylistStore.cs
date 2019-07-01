using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Shared
{
    public class PlaylistStore
    {
        private FoxsoundiContext dbContext;
        private PlayerStore playerStore;

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

            currentPlayer.PersonnalPlaylists.Add(new Database.Playlist
            {
                CreatedBy = currentPlayer,
                Description = dto.description,
                Name = dto.name
            });
            dbContext.Update(currentPlayer);
            
            return dto;
        }
    }
}
