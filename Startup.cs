using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nancy.Owin;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public string Secret { get; set; }
        public string ClientId { get; set; }
        private readonly ILogger logger;
        public Startup(IHostingEnvironment env, ILogger<Startup> logger)
        {
            this.logger = logger;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json",
                             optional: false,
                             reloadOnChange: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Secret = Configuration["Spotify:client_secret"];
            //ClientId = Configuration["Spotify:client_id"];
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                logger.LogInformation("Development Mode");
            }
            app.UseOwin(b => b.UseNancy(options => options.Bootstrapper = new MyBootstrapper(Configuration, logger)));

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
