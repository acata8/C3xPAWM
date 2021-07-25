using C3xPAWM.Models.Options;
using C3xPAWM.Models.Services.Application;
using C3xPAWM.Models.Services.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

namespace C3xPAWM
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddResponseCaching();  

            services.AddControllersWithViews(options => {
                var homeProfile = new CacheProfile();
                var elencoProfile = new CacheProfile();
                //Riempio location e duration tramite il bind
                configuration.Bind("ResponseCache:Home", homeProfile);
                configuration.Bind("ResponseCache:Elenco", elencoProfile);
                options.CacheProfiles.Add("Home", homeProfile);
                options.CacheProfiles.Add("Elenco", elencoProfile);
            });
            services.AddTransient<INegoziService, EfCoreNegoziService>();
            services.AddTransient<ICachedNegoziService, MemoryCacheNegoziService>();
            services.AddDbContextPool<C3PAWMDbContext>(optionsBuilder =>
            {
                string connectionString = configuration.GetSection("ConnectionsStrings").GetValue<string>("Default");
                optionsBuilder.UseSqlite(connectionString);
            });

            //Options
            services.Configure<ConnectionsStringsOptions>(configuration.GetSection("ConnectionsStrings"));
            services.Configure<NegoziOptions>(configuration.GetSection("Negozi"));
            services.Configure<CacheOptions>(configuration.GetSection("MemoryCache"));
            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            //Setting per BrowserSync
            try
            {
                File.WriteAllText("browsersync-update.txt", DateTime.Now.ToString());
            }catch { }
                
            
                
            }
            else
            {
                app.UseExceptionHandler("/Error");
               
            }
            
            app.UseStaticFiles();

            //app.UseResponseCaching();

            app.UseRouting();

           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{x?}");
            });


        }
    }
}
