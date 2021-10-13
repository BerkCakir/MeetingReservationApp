using MeetingReservationApp.Managers.Abstract;
using MeetingReservationApp.Managers.AutoMapper;
using MeetingReservationApp.Managers.Concrete;
using MeetingReservationApp.Managers.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace MeetingReservationApp.RoomReservationApi
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
            //IdentityServer
            var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(c =>
            {
                c.Authority = Configuration["IdentityServerURL"];
                c.Audience = "roomreservation";
                c.RequireHttpsMetadata = false;
            });
            services.AddControllers(c =>
            {
                c.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));  //IdentityServer
            }).AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // newtonsoft added for loop handling error
            services.AddAutoMapper(typeof(RoomReservationProfile));
            services.LoadMyServices(connectionString: Configuration.GetConnectionString("LocalDB"));
            services.AddScoped<IRoomReservationService, RoomReservationManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
