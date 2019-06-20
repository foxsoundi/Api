using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spotify.Connections
{
    public class ArtistConnection
    {
        private HttpClient client;

        public ArtistConnection(ref HttpClient client)
        {
            this.client = client;
        }

        public async Task<string> GetArtists()
        {
            Uri url = new Uri("https://api.spotify.com/v1/artists/");
            HttpResponseMessage response = await client.GetAsync(url);
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }

        public async Task<string> GetArtist(string artistId)
        {
            Uri url = new Uri("https://api.spotify.com/v1/artists/{artistId}");
            HttpResponseMessage response = await client.GetAsync(url);
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }

        public async Task<string> GetArtistAlbums(string artistId)
        {
            Uri url = new Uri("https://api.spotify.com/v1/artists/{artistId}/albums");
            HttpResponseMessage response = await client.GetAsync(url);
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }

        public async Task<string> GetArtistTops(string artistId)
        {
            Uri url = new Uri("https://api.spotify.com/v1/artists/{artistId}/top-tracks");
            HttpResponseMessage response = await client.GetAsync(url);
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }

        public async Task<string> GetArtistRelateds(string artistId)
        {
            Uri url = new Uri("https://api.spotify.com/v1/artists/{artistId}/top-tracks");
            HttpResponseMessage response = await client.GetAsync(url);
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }
    }
}