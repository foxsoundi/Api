using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.Conventions;
using Nancy.Swagger.Services;
using Nancy.TinyIoc;
using Swagger.ObjectModel;

namespace Api
{
    public class MyBootstrapper : DefaultNancyBootstrapper
    {
        private readonly IConfiguration Configuration;
        private readonly MySecrets Secrets;
        public MyBootstrapper(IConfiguration configuration)
        {
            Configuration = configuration;
            Secrets = new MySecrets(configuration);
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            // your customization goes here
            SwaggerMetadataProvider.SetInfo("Nancy Swagger Example", "v1.0", "Some open api", new Contact()
            {
                EmailAddress = "",
                Name = "FoxSoundi Team",
                Url = ""
            }, "");
            base.ApplicationStartup(container, pipelines);
        }

        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);
            environment.AddValue(Secrets);
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("Swagger-ui"));
        }
    }
}

