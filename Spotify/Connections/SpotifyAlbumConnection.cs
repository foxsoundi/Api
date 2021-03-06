﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace Spotify.Connections
{
    public class SpotifyAlbumConnection
    {
        private readonly HttpClient client;

        public SpotifyAlbumConnection(HttpClient client)
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

        public async Task<string> GetAlbums(string[] albumIds)
        {
            string parsedIds = albumIds.Join(",");
            Uri playlistUrl = new Uri($"https://api.spotify.com/v1/albums?ids={parsedIds}");
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