using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarParts.API.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CarParts.API
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

            Infrastructure.Configure
                        .ConfigureServices(services, Configuration.GetConnectionString("carPartsConn"));

            services.AddControllers().AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling =
                       Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudianySettings"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = new SymmetricSecurityKey(
                               Encoding.UTF8.GetBytes(Configuration
                                        .GetSection("AppSettings:Token").Value)),
                           ValidateIssuer = false,
                           ValidateAudience = false
                       };
                   });
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
