using Microsoft.AspNetCore.Hosting;

namespace C3xPAWM.Areas.Identity
{
    public class IdentityHostingStartup: IHostingStartup
    {
     
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}