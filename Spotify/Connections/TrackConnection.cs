using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spotify.Connections
{
    public class TrackConnection
    {
        private readonly HttpClient client;

        public TrackConnection(HttpClient client)
        {
            this.client = client;
        }

        public async Task<string> GetTracks()
        {
            Uri trackUrl = new Uri("https://api.spotify.com/v1/tracks");
            HttpResponseMessage response = await client.GetAsync(trackUrl);
            string content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task<string> GetAudioFeature()
        {
            Uri trackUrl = new Uri("https://api.spotify.com/v1/audio-features");
            HttpResponseMessage response = await client.GetAsync(trackUrl);
            string content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task<string> GetTrack(string id)
        {
            Uri trackUrl = new Uri($"https://api.spotify.com/v1/tracks/{id}?market=FR");
            HttpResponseMessage response = await client.GetAsync(trackUrl);
            string content = await response.Content.ReadAsStringAsync();

            return content;
        }
    }
}