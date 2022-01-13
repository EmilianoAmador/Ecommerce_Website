using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)    // This is a constructor. It gives us access to our configuration that is being injected into our startup class.
        {                                               // Not a typical way of how inject things into our classes this is Microsofts version.
            _config = config;
        }

        // public IConfiguration Configuration { get; }  erased bc 012 says non conventional // this is a method. It configures services. It allows us to inject any service we add to our app into other classes.

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppIdentityDbContext>(x => x.UseSqlite(_config.GetConnectionString("IdentityConnection")));

            services.AddSingleton<IConnectionMultiplexer>(c => {
                var configuration = ConfigurationOptions.Parse(_config.GetConnectionString("Redis"), true);
                return ConnectionMultiplexer.Connect(configuration);
                });
            services.AddApplicationServices();
            services.AddIdentityServices(_config);
            services.AddSwaggerDocumentation();

            // Enables CORS Support: tells our client application not to return a header if its running on an unsecured port.
            services.AddCors(opt => 
                {
                opt.AddPolicy("CorsPolicy", policy => 
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Middleware that configures information passing through pipes from or to an API.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();     // Miiddleware to use exception handling middleware in Middleware folder

            app.UseSwaggerDocumentation();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            // below youll find some middleswares to help handle some web features.
            app.UseHttpsRedirection();       // redirects to https in case someone types http instead of https

            app.UseRouting();             // helps us access API end points by routing them

            app.UseStaticFiles();
            
            // CORS Middleware
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>   // middleware that helps app know which ends points are available to route
            {
                endpoints.MapControllers();
            });
        }
    }
}
