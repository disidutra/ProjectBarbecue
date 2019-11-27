using ApplicationCore.Interfaces.Repositorys;
using Barbecue.Infrastructure.Data;
using Barbecue.Infrastructure.Repositorys;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Barbecue.ApplicationCore.Interfaces.Services;
using Barbecue.ApplicationCore.Services;
using Barbecue.ApplicationCore.Interfaces.Repositorys;

namespace Barbecue.WebAPI
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
            services.AddScoped(typeof(IEfBaseRepository<>), typeof(EfBaseRepository<>));            
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventRepository, EventRepository>();


            services.AddDbContext<EfBaseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DBConnection"), b => b.MigrationsAssembly("Barbecue.Infrastructure"));
            });
            
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API Barbecue", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API Barbecue V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
