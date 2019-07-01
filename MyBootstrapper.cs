using System.Net.Http;
using Api;
using Api.Modules;
using Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.TinyIoc;
using Shared;
using Spotify;
using Spotify.Connections;

namespace Api
{
    public class MyBootstrapper : DefaultNancyBootstrapper
    {
        private readonly IConfiguration Configuration;
        private readonly SpotifySecrets spotifySecrets;
        private readonly ILogger logger;
        private IApplicationBuilder app;

        public MyBootstrapper(IConfiguration configuration, ILogger logger, IApplicationBuilder app)
        {
            this.app = app;
            Configuration = configuration;
            this.logger = logger;
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            void SpotifyStartup(TinyIoCContainer tinyIoCContainer)
            {
                tinyIoCContainer.Register<HttpClient>().AsSingleton();
                tinyIoCContainer.Register<Access>().AsSingleton();
                tinyIoCContainer.Register<SpotifyConnection>()
                    .UsingConstructor(() => new SpotifyConnection(tinyIoCContainer.Resolve<HttpClient>(), tinyIoCContainer.Resolve<Access>()))
                    .AsSingleton();
                tinyIoCContainer.Register<SpotifyTrackConnection>()
                    .UsingConstructor(() => new SpotifyTrackConnection(tinyIoCContainer.Resolve<HttpClient>())).AsSingleton();
                tinyIoCContainer.Register<SpotifyGenreConnection>()
                    .UsingConstructor(() => new SpotifyGenreConnection(tinyIoCContainer.Resolve<HttpClient>())).AsSingleton();
                tinyIoCContainer.Register<SpotifyPlaylistConnection>()
                    .UsingConstructor(() => new SpotifyPlaylistConnection(tinyIoCContainer.Resolve<HttpClient>())).AsSingleton();
                tinyIoCContainer.Register<SpotifyArtistConnection>()
                    .UsingConstructor(() => new SpotifyArtistConnection(tinyIoCContainer.Resolve<HttpClient>())).AsSingleton();
                tinyIoCContainer.Register<SpotifyAlbumConnection>()
                    .UsingConstructor(() => new SpotifyAlbumConnection(tinyIoCContainer.Resolve<HttpClient>())).AsSingleton();
                //tinyIoCContainer.Register<FoxsoundiContext>();
            }
            //container.BuildUp(services);
            SpotifyStartup(container);

            void DatabaseStartup()
            {
                DbContextOptionsBuilder<FoxsoundiContext> dbContextOptionBuilder = new DbContextOptionsBuilder<FoxsoundiContext>();
                dbContextOptionBuilder.UseSqlServer(Configuration.GetConnectionString("FoxsoundiDb"));
                DbContextOptions<FoxsoundiContext> dbOptions = dbContextOptionBuilder.Options;
                container.Register<DbContextOptions<FoxsoundiContext>>(dbOptions);
                container.Register<FoxsoundiContext>()
                    .UsingConstructor(() => new FoxsoundiContext(container.Resolve<DbContextOptions<FoxsoundiContext>>()))
                    .AsSingleton();
                Initializer.Initialize(container.Resolve<FoxsoundiContext>());
            }
            DatabaseStartup();

            container.Register<PlayerStore>().UsingConstructor(() => new PlayerStore(container.Resolve<FoxsoundiContext>())).AsSingleton();
            container.Register<PlaylistStore>().UsingConstructor(() => new PlaylistStore(container.Resolve<FoxsoundiContext>(), container.Resolve<PlayerStore>())).AsSingleton();
            container.Register<PlayerConnection>()
                .UsingConstructor(() => new PlayerConnection(container.Resolve<PlayerStore>())).AsSingleton();
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
            environment.AddValue(spotifySecrets);
        }
    }
}

