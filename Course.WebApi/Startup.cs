using Course.WebApi.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Course.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Course.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddNewtonsoftJson();
            
            services.AddDbContext<EmployDbContext>(options =>
            {
                // 透過Configuration.GetConnectionString方法取得連線字串
                options.UseMySql(Configuration.GetConnectionString("employ_db"));
            });

            services.AddScoped<IEmployRepo, EmployRepo>();


        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
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
