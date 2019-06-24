using System;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("SpotifyTest")]
namespace Api.Spotify
{
    internal class Access
    {
        public TimeSpan ExpireIn { get; }
        public string Token { get; }
        public string Scope { get; }
        public string Type { get; }

        private event EventHandler triggerReconnect;
        private readonly Action connect;
        private Thread reconnectThread;
        internal Access(AccessDto dto, Action connect)
        {
            this.ExpireIn = TimeSpan.FromSeconds(dto.ExpireInSeconds);
            this.Token = dto.Token;
            this.Scope = dto.Scope;
            this.Type = dto.Type;
            this.connect = connect;

            reconnectThread = new Thread(async () => await ReconnectIn(ExpireIn));
            triggerReconnect += (o, e) =>
            {
                reconnectThread.Join();
                reconnectThread = new Thread(async () => await ReconnectIn(ExpireIn));
                reconnectThread.Start();
            };
        }

        public AuthenticationHeaderValue GetAuthentication()
        {
            reconnectThread.Start();
            return new AuthenticationHeaderValue(Type, Token);
        }

        private async Task ReconnectIn(TimeSpan timespan)
        {
            await Task.Delay(timespan - TimeSpan.FromSeconds(10));
            connect();
            triggerReconnect?.Invoke(this, null);
        }
    }
}