using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spotify.Connections
{
    public class GenreConnection
    {
        private readonly HttpClient client;

        public GenreConnection(ref HttpClient client)
        {
            this.client = client;
        }

        public async Task<string> GetGenres()
        {
            Uri GenresUrl = new Uri($"https://api.spotify.com/v1/browse/categories");
            HttpResponseMessage response = await client.GetAsync(GenresUrl);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetGenres(string genreId)
        {
            Uri GenresUrl = new Uri($"https://api.spotify.com/v1/browse/categories/{genreId}");
            HttpResponseMessage response = await client.GetAsync(GenresUrl);
            return await response.Content.ReadAsStringAsync();
        }
    }
}