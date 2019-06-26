using System;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using Api;
using Api.Spotify;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class SpotifyTest
    {
        [Test]
        public void Should_connect_once()
        {
            AccessDto dto = new AccessDto
            {
                ExpireInSeconds = 15,
                Scope = string.Empty,
                Token = "token",
                Type = "Type"
            };
            Mock<Action> connect = new Mock<Action>();

            Access access = new Access(dto, connect.Object);
            access.GetAuthentication();

            Thread.Sleep(TimeSpan.FromSeconds(6));

            connect.Verify(x => x.Invoke(), Times.AtLeastOnce);
        }

        [Test]
        public void Should_connect_multiple_times()
        {
            AccessDto dto = new AccessDto
            {
                ExpireInSeconds = 11,
                Scope = string.Empty,
                Token = "token",
                Type = "Type"
            };
            Mock<Action> connect = new Mock<Action>();

            Access access = new Access(dto, connect.Object);
            access.GetAuthentication();

            Thread.Sleep(TimeSpan.FromSeconds(4));

            connect.Verify(x => x.Invoke(), Times.AtLeast(3));
        }
    }
}