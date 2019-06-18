using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Api.Spotify
{
    [DataContract]
    public class AccessDto
    {
        [DataMember(Name = "access_token")]
        public string Token { get; set; }

        [DataMember(Name = "token_type")]
        public string Type { get; set; }

        [DataMember(Name = "expires_in")]
        public int ExpireInSeconds { get; set; }

        [DataMember(Name = "scope")]
        public string Scope { get; set; }
    }
}
