using System;
using System.Net;
using System.Threading.Tasks;

namespace Api.Spotify
{
    internal class Access
    {
        public TimeSpan ExpireIn { get; }
        public string Token { get; }
        public string Scope { get; }
        public string Type { get; }

        public event EventHandler expired;

        internal Access(AccessDto dto)
        {
            this.ExpireIn = TimeSpan.FromSeconds(dto.ExpireIn);
            this.Token = dto.Token;
            this.Scope = dto.Scope;
            this.Type = dto.Type;
        }
    }
}