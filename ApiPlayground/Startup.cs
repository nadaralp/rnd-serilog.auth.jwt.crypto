using ApiPlayground.Infrastructure.Options;
using ApiPlayground.Infrastructure.Security.Hashing;
using ApiPlayground.Infrastructure.Security.Jwt;
using ApiPlayground.Infrastructure.Security.Policies;
using ApiPlayground.InMemoryCache;
using ApiPlayground.Middlewares;
using ApiPlayground.Services;
using ClassLibrary1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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


            services.AddAssemblyOptions(Configuration);

            //services.AddAuthentication(AuthenticationScheme)
            services.AddMemoryCache();

            // Service 1 dependencies
            services.AddClassLibrary1DIConfiguration();


            services.AddSingleton<SeedUsers>();
            services.AddSingleton<HashBuilder>();
            services.AddSingleton<SaltGenerator>();
            services.AddSingleton<PasswordService>();
            services.AddSingleton<ITokenService, JwtTokenService>();
            services.AddSingleton<IUserService, InMemoryUserService>();
            services.AddSingleton<IAuthenticationService, AuthenticationService>();

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
