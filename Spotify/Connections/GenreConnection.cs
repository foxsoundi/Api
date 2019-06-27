﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Spotify;
using Newtonsoft.Json;

namespace Spotify.Connections
{
    public class GenreConnection
    {
        private readonly HttpClient client;

        public GenreConnection(HttpClient client)
        {
            this.client = client;
        }

        public async Task<GenreDto> GetGenres()
        {
            Uri GenresUrl = new Uri($"https://api.spotify.com/v1/browse/categories");
            HttpResponseMessage response = await client.GetAsync(GenresUrl);
            GenreDto result = JsonConvert.DeserializeObject<GenreDto>(await response.Content.ReadAsStringAsync());
            result.categories.items = result.categories.items.Select(item =>
            {
                item.icons = item.icons.Select(icon =>
                {
                    if (!icon.height.HasValue || !icon.width.HasValue)
                    {
                        icon.height = 274;
                        icon.width = 274;
                    }

                    return icon;
                }).ToList();
                return item;
            }).ToList();
            return result;
        }

        public async Task<string> GetGenres(string genreId)
        {
            Uri GenresUrl = new Uri($"https://api.spotify.com/v1/browse/categories/{genreId}");
            HttpResponseMessage response = await client.GetAsync(GenresUrl);
            return await response.Content.ReadAsStringAsync();
        }
    }
}