using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class MySecrets
    {
        public string Id { get; set; }
        public string Secret { get; set; }

        public MySecrets(IConfiguration configuration)
        {
            Secret = configuration["Spotify:Secret"];
            Id = configuration["Spotify:ClientId"];
        }
    }
}
