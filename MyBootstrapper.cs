using System.Net.Http;
using Api;
using Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.TinyIoc;
using Spotify.Connections;

namespace Api
{
    public class MyBootstrapper : DefaultNancyBootstrapper
    {
        private readonly IConfiguration Configuration;
        private readonly MySecrets Secrets;
        private readonly ILogger logger;
        public MyBootstrapper(IConfiguration configuration, ILogger logger)
        {
            Configuration = configuration;
            this.logger = logger;
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            void SpotifyStartup(TinyIoCContainer tinyIoCContainer)
            {
                tinyIoCContainer.Register<HttpClient>().AsSingleton();
                tinyIoCContainer.Register<SpotifyConnection>()
                    .UsingConstructor(() => new SpotifyConnection(tinyIoCContainer.Resolve<HttpClient>()))
                    .AsSingleton();
                tinyIoCContainer.Register<TrackConnection>()
                    .UsingConstructor(() => new TrackConnection(tinyIoCContainer.Resolve<HttpClient>())).AsSingleton();
                tinyIoCContainer.Register<GenreConnection>()
                    .UsingConstructor(() => new GenreConnection(tinyIoCContainer.Resolve<HttpClient>())).AsSingleton();
                tinyIoCContainer.Register<PlaylistConnection>()
                    .UsingConstructor(() => new PlaylistConnection(tinyIoCContainer.Resolve<HttpClient>())).AsSingleton();
                tinyIoCContainer.Register<ArtistConnection>()
                    .UsingConstructor(() => new ArtistConnection(tinyIoCContainer.Resolve<HttpClient>())).AsSingleton();
                tinyIoCContainer.Register<AlbumConnection>()
                    .UsingConstructor(() => new AlbumConnection(tinyIoCContainer.Resolve<HttpClient>())).AsSingleton();
                tinyIoCContainer.Register<FoxsoundiContext>();
            }

            //container.Register<FoxsoundiContext>().AsSingleton();
            SpotifyStartup(container);

            //CORS Enable
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");

            });
            pipelines.OnError += (ctx, ex) =>
            {
                logger.LogError(ex.Message);
                return ex;
            };
        }

        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);
            environment.AddValue(Secrets);
        }
    }

}

