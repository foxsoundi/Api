using System.Linq;
using System.Net.Http;
using Api;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Configuration;
using Nancy.TinyIoc;
using Shared;
using Spotify;
using Spotify.Connections;

namespace Api
{
    public class MyBootstrapper : AutofacNancyBootstrapper
    {
        private readonly IConfiguration Configuration;
        private readonly SpotifySecrets spotifySecrets;
        private readonly ILogger logger;
        private IServiceCollection services;

        public MyBootstrapper(IConfiguration configuration, ILogger logger, IServiceCollection services)
        {
            this.services = services;
            Configuration = configuration;
            this.logger = logger;
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            base.ConfigureApplicationContainer(container);
            ContainerBuilder SpotifyStartup(ILifetimeScope autofacContainer)
            {
                ContainerBuilder builder = new ContainerBuilder();

                builder.RegisterType<HttpClient>().SingleInstance();
                //builder.RegisterType<HttpClient>().AsSingleton();
                builder.RegisterType<SpotifyConnection>()
                    .UsingConstructor(() => new SpotifyConnection(autofacContainer.Resolve<HttpClient>()))
                    .SingleInstance();
                builder.RegisterType<SpotifyTrackConnection>()
                    .UsingConstructor(() => new SpotifyTrackConnection(autofacContainer.Resolve<HttpClient>())).SingleInstance();
                builder.RegisterType<SpotifyGenreConnection>()
                    .UsingConstructor(() => new SpotifyGenreConnection(autofacContainer.Resolve<HttpClient>())).SingleInstance();
                builder.RegisterType<SpotifyPlaylistConnection>()
                    .UsingConstructor(() => new SpotifyPlaylistConnection(autofacContainer.Resolve<HttpClient>())).SingleInstance();
                builder.RegisterType<SpotifyArtistConnection>()
                    .UsingConstructor(() => new SpotifyArtistConnection(autofacContainer.Resolve<HttpClient>())).SingleInstance();
                builder.RegisterType<SpotifyAlbumConnection>()
                    .UsingConstructor(() => new SpotifyAlbumConnection(autofacContainer.Resolve<HttpClient>())).SingleInstance();

                return builder;
                //builder.RegisterType<FoxsoundiContext>();
            }

            //container.BuildUp(services);
            ContainerBuilder thisBuilder = SpotifyStartup(container);
            container.Update(b =>
            {
                b.Populate(services);
            });
            thisBuilder.RegisterType<FoxsoundiContext>().SingleInstance();
            Initializer.Initialize(container.Resolve<FoxsoundiContext>());
            thisBuilder.RegisterType<Store>().UsingConstructor(() => new Store(container.Resolve<FoxsoundiContext>())).SingleInstance();
            thisBuilder.RegisterType<PlayerConnection>()
                .UsingConstructor(() => new PlayerConnection(container.Resolve<Store>())).SingleInstance();
        }
    

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
         
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

