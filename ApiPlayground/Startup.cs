using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPlayground.Infrastructure.Security.Hashing;
using ApiPlayground.Infrastructure.Security.Policies;
using ApiPlayground.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ApiPlayground
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddAuthentication(AuthenticationScheme)
            services.AddSingleton<HashBuilder>();

            services.AddSecurtyPolicies();
            services.AddControllers()
                .AddJsonOptions(configure =>
                {
                    configure.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("middleware that hijacks and returns");
            //});

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Custom Middleware
            app.UseMiddleware<AddPrincipalMiddleware>();

            // Will show the request for pages
            app.UseSerilogRequestLogging();

            app.UseRouting();

            //app.UseCookiePolicy();

            // app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
