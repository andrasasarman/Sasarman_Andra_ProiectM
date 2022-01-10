using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sasarman_Andra_Proiect.Data;

[assembly: HostingStartup(typeof(Sasarman_Andra_Proiect.Areas.Identity.IdentityHostingStartup))]
namespace Sasarman_Andra_Proiect.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(

               context.Configuration.GetConnectionString("IdentityContextConnection")));
                services.AddIdentity<IdentityUser, IdentityRole>(options =>
               options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultUI()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);

            });
        }
    }
}