using BusinessLayer.BusinessLogic;
using BusinessLayer.Interfaces;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<CoreContext>(options =>
                {
                    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
                    
                }).AddSingleton(Configuration.GetConnectionString("DefaultConnection"));
            //services.AddSingleton<IDnaSequenceQuery,DnaSequenceQuery>();
            services.AddScoped<IDnaSequenceQuery, DnaSequenceQuery>();


            #endregion Context SQL Postgres

            #region Register (dependency injection)

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
                endpoints.MapControllers();
            });
        }

    }
}
