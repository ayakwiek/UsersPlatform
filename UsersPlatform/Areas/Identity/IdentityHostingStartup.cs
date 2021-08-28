using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersPlatform.Areas.Identity.Data;
using UsersPlatform.Data;

[assembly: HostingStartup(typeof(UsersPlatform.Areas.Identity.IdentityHostingStartup))]
namespace UsersPlatform.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                //services.AddDbContext<UsersPlatformContext>(options =>
                //    options.UseSqlServer(
                //        context.Configuration.GetConnectionString("UsersPlatformContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options => {
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 3;
                }).AddRoles<IdentityRole>().AddEntityFrameworkStores<UsersPlatformContext>();
            });
        }
    }
}