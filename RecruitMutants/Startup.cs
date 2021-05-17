using BusinessLayer.BusinessLogic;
using BusinessLayer.Interfaces;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RecruitMutants
{

    /// <summary>
    /// Class Startup.
    /// </summary>
    /// <remarks>Julieth Gil</remarks>
    [ExcludeFromCodeCoverage]
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
            #region Context Postgres
            //other service configuration goes here...
            //pull in connection string
            string connectionString = null;
            string envVar = Environment.GetEnvironmentVariable("DATABASE_URL");
            if (string.IsNullOrEmpty(envVar))
            {
                connectionString = Configuration.GetConnectionString("DefaultConnection");
            }
            else
            {
                var uri = new Uri(envVar);
                var username = uri.UserInfo.Split(':')[0];
                var password = uri.UserInfo.Split(':')[1];
                var host = envVar.Split("@")[1].Split(":")[0];

                connectionString =
                "; Host= " + host +
                "; Database=" + uri.AbsolutePath.Substring(1) +
                "; Username=" + username +
                "; Password=" + password +
                "; Port=" + uri.Port +
                "; SSL Mode=Require; Trust Server Certificate=true;";
            }
            services.AddDbContext<CoreContext>(opt =>
                  opt.UseNpgsql(connectionString)
            ).AddSingleton(connectionString);


            #endregion Context SQL Postgres

            #region Register (dependency injection)

            services.AddScoped<IDnaSequenceQuery, DnaSequenceQuery>();
            services.AddScoped<IDnaSequenceLogic, DnaSequenceLogic>();
            services.AddControllers();
            
            #endregion Register (dependency injection)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello Mutants!");
                });

                endpoints.MapControllers();
            });
        }

    }
}
