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
using Microsoft.AspNetCore.Identity;
using C3xPAWM.Models.Entities;
using C3xPAWM.Customizations.Identity;

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
            services.AddResponseCaching();
            services.AddRazorPages();

            services.AddControllersWithViews(options => {
                var homeProfile = new CacheProfile();
                var elencoProfile = new CacheProfile();
                //Riempio location e duration tramite il bind
                configuration.Bind("ResponseCache:Home", homeProfile);
                configuration.Bind("ResponseCache:Elenco", elencoProfile);
                options.CacheProfiles.Add("Home", homeProfile);
                options.CacheProfiles.Add("Elenco", elencoProfile);
            })
            #if DEBUG
            .AddRazorRuntimeCompilation()
            #endif
            ;

            
            services.AddDefaultIdentity<ApplicationUser>(
                    option => {
                    option.Password.RequiredLength = 8;
                    option.Password.RequireDigit = true;
                    option.Password.RequireUppercase = true;
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequiredUniqueChars = 0;
                    option.Password.RequireLowercase = true;

                    option.SignIn.RequireConfirmedAccount = true;
                    
                })
                .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<C3PAWMDbContext>();
            

            services.AddTransient<INegoziService, EfCoreNegoziService>();
            services.AddTransient<ICorriereService, EfCoreCorrieriService>();
                     
           services.AddDbContextPool<C3PAWMDbContext>(optionsBuilder =>
            {
                string connectionString = configuration.GetSection("ConnectionsStrings").GetValue<string>("Default");
                optionsBuilder.UseSqlite(connectionString);
            });


            //Options
            services.Configure<ConnectionsStringsOptions>(configuration.GetSection("ConnectionsStrings"));
            services.Configure<ElencoOptions>(configuration.GetSection("Elenco"));
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
                File.WriteAllText("bin/browsersync-update.txt", DateTime.Now.ToString());
            }catch { }
                

            }
            else
            {
                app.UseExceptionHandler("/Error");
               
            }
            

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseResponseCaching();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });


        }
    }
}
