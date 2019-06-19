using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Spotify
{
    public class PlaylistConnection
    {
        private readonly HttpClient client;

        public PlaylistConnection(ref HttpClient client)
        {
            this.client = client;
        }

        public async Task<string> GetPlaylist(string playlistId)
        {
            Uri playlistUrl = new Uri($"https://api.spotify.com/v1/playlists/{playlistId}");
            Uri url = new Uri($"https://api.spotify.com/v1/browse/categories/{genreId}");
            HttpResponseMessage response = await client.GetAsync(playlistUrl);
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }
    }
}