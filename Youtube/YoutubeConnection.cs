﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace Youtube
{
    public class YoutubeConnection
    {
        private YouTubeService youtubeService;

        public YoutubeConnection()
        {
            
        }

        public void AddAndUseSecret(YoutubeSecrets apiKeys)
        {
            this.youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKeys.YoutubeId,
                ApplicationName = this.GetType().ToString()
            });
        }

        public async Task<string> GetVideoIdOf(string songToSearch)
        {
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = songToSearch; // Replace with your songToSearch term.
            searchListRequest.MaxResults = 50;

            // Call the songToSearch.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> videos = new List<string>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            //searchListResponse.
            return searchListResponse.Items.First(i => i.Id.Kind == "youtube#video").Id.VideoId;
        }
    }
}
