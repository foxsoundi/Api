using Api;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.TinyIoc;

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
        }

        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);
            environment.AddValue(Secrets);
        }
    }

}

