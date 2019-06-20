using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spotify.Connections
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
            HttpResponseMessage response = await client.GetAsync(playlistUrl);
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }
    }
}