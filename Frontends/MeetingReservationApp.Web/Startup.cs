using MeetingReservationApp.Web.Handler;
using MeetingReservationApp.Web.Models;
using MeetingReservationApp.Web.Services.Abstract;
using MeetingReservationApp.Web.Services.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingReservationApp.Web
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
            services.Configure<ServiceApiSettings>(Configuration.GetSection("ServiceApiSettings"));
            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));
            services.AddHttpContextAccessor();
            services.AddHttpClient<IIdentityService, IdentityService>();
            services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
            services.AddHttpClient<IUserService, UserService>(o =>
            {
                o.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
            services.AddHttpClient<IRoomReservationService, RoomReservationService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.RoomReservation.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
            services.AddHttpClient<IInventoryReservationService, InventoryReservationService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.InventoryReservation.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();
            // login cookie settings 
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
            {    
                opts.LoginPath = "/Auth/SignIn";
                opts.ExpireTimeSpan = TimeSpan.FromDays(60);
                opts.SlidingExpiration = true;
                opts.Cookie.Name = "webapplicationcookie";
            });

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
