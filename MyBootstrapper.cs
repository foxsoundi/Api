using Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger logger;
        public MyBootstrapper(IConfiguration configuration, ILogger logger)
        {
            Configuration = configuration;
            this.logger = logger;
            Secrets = new MySecrets(configuration);
            if (string.IsNullOrEmpty(Secrets.Id))
                logger.LogError("Secret Id is missing");

            if (string.IsNullOrEmpty(Secrets.Secret))
                logger.LogError("Secret secret is missing");
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
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

