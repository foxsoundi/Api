using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Shared;

namespace Api
{
    public class SpotifySecrets : ISecret
    {
        public string SpotifyId { get; set; }
        public string SpotifySecret { get; set; }

        public SpotifySecrets() { }

        public AuthenticationHeaderValue GetBasicAuthenticationHeaderValue() => new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{SpotifyId}:{SpotifySecret}")));

    }
}
