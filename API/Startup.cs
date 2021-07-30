using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

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
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddControllers();
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Middleware that configures information passing through pipes from or to an API.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
            // below youll find some middleswares to help handle some web features.
            app.UseHttpsRedirection();       // redirects to https in case someone types http instead of https

            app.UseRouting();             // helps us access API end points by routing them

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>   // middleware that helps app know which ends points are available to route
            {
                endpoints.MapControllers();
            });
        }
    }
}
