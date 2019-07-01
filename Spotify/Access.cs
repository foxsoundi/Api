using System;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Api.Spotify;

[assembly: InternalsVisibleTo("SpotifyTest")]
namespace Spotify
{
    public class Access
    {
        public TimeSpan ExpireIn { get; }
        public string Token { get; }
        public string Scope { get; }
        public string Type { get; }

        private event EventHandler triggerReconnect;
        private readonly Action connect;
        private Thread reconnectThread;
        public bool IsConnected { get; set; } = false;

        public Access()
        {
            IsConnected = false;
        }

        public Access(AccessDto dto)//, Action connect)
        {
            this.ExpireIn = TimeSpan.FromSeconds(dto.ExpireInSeconds);
            this.Token = dto.Token;
            this.Scope = dto.Scope;
            this.Type = dto.Type;
            //this.connect = connect;

            //reconnectThread = new Thread(async () => await ReconnectIn(ExpireIn));
            //triggerReconnect += (o, e) =>
            //{
            //    reconnectThread.Join();
            //    IsConnected = false;
            //    reconnectThread = new Thread(async () => await ReconnectIn(ExpireIn));
            //    reconnectThread.Start();
            //};
        }

        public AuthenticationHeaderValue GetAuthentication()
        {
            //reconnectThread.Start();
            return new AuthenticationHeaderValue(Type, Token);
        }

        private async Task ReconnectIn(TimeSpan timespan)
        {
            await Task.Delay(timespan - TimeSpan.FromSeconds(10));
            //await Task.Delay(TimeSpan.);
            connect();
            triggerReconnect?.Invoke(this, null);
        }
    }
}