using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.IServices;
using WebApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Bsm.WebApi.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Bsm.WebApi.IRepositories;
using Bsm.WebApi.Repositories;
using Bsm.WebApi.IServices;
using Bsm.WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            //add utilities here
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.Configure<SecuritySettings>(Configuration.GetSection("SecuritySettings"));
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            //add services here
            services.AddTransient<ITemplateService, TemplateService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<IHierarchyService,HierarchyService>();
            services.AddTransient<IHierarchyUserService, HierarchyUserService>();
            services.AddTransient<IHierarchyVehicleService, HierarchyVehicleService>();
            services.AddTransient<IDbLoggerService, DbLoggerService>();


            //add repository here
            services.AddTransient<ISecurityRepository, SecurityRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            services.AddTransient<IHierarchyRepository, HierarchyRepository>();
            services.AddTransient<IHierarchyUserRepository, HierarchyUserRepository>();
            services.AddTransient<IHierarchyVehicleRepository, HierarchyVehicleRepository>();
            services.AddTransient<IDbLoggerRepository, DbLoggerRepository>();

            services.AddTransient<ISqlHelper, SqlHelper>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BSMwireless1234567890")),
                    ValidAudience = "http://localhost:54338",
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = "http://localhost:54338"
                }
            });

            loggerFactory.AddFile("c:/Logs/webapi/webapi-{Date}.txt");
            app.UseMvc();
        }

        
    }
}
