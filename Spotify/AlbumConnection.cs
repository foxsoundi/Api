using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Spotify
{
    public class AlbumConnection
    {
        private HttpClient client;

        public AlbumConnection(HttpClient client)
        {
            this.client = client;
        }

        public async Task<string> GetAlbum(string albumId)
        {
            Uri playlistUrl = new Uri($"https://api.spotify.com/v1/albums/{albumId}");
            HttpResponseMessage response = await client.GetAsync(playlistUrl);
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }

        public async Task<string> GetAlbums()
        {
            Uri playlistUrl = new Uri($"https://api.spotify.com/v1/albums");
            HttpResponseMessage response = await client.GetAsync(playlistUrl);
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }

        public async Task<string> GetAlbumTracks(object albumId)
        {
            Uri playlistUrl = new Uri($"https://api.spotify.com/v1/albums/{albumId}/tracks");
            HttpResponseMessage response = await client.GetAsync(playlistUrl);
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }
    }
}