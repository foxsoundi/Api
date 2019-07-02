using System;
using System.Collections.Generic;
using System.Linq;
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

            Playlist newPlaylist = new Database.Playlist
            {
                CreatedBy = currentPlayer,
                Description = dto.description,
                Name = dto.name
            };
            currentPlayer.PersonnalPlaylists.Add(newPlaylist);
            dbContext.Update(currentPlayer);
            await dbContext.SaveChangesAsync();
            dto.owner = new Owner { display_name = $"{currentPlayer.LastName + currentPlayer.FirstName}" };
            dto.id = newPlaylist.Id.ToString();
            playerStore.Refresh(currentPlayer);
            return dto;
        }

        public async Task<PlaylistsRootObject> GetAllPlaylist(Guid sessionId)
        {
            Database.Player player = playerStore.GetLoggedPlayer(sessionId);
            PlaylistsRootObject playlistRootObject = new PlaylistsRootObject();
            playlistRootObject.playlists = new PlaylistsDto();
            playlistRootObject.playlists.items = player.PersonnalPlaylists.Select(x => new PlaylistDto
            {
                id = x.Id.ToString(),
                description = x.Description,
                name = x.Name,
            }).ToList();
            return playlistRootObject;
        }
    }
}
