using AutoMapper;
using M13.Domain.AppSettings;
using M13.Domain.Interfaces;
using M13.Domain.MapperProfiles;
using M13.Domain.Services;
using M13.Infrastructure.Data;
using M13.Infrastructure.RepositoryServices;
using M13.InterviewProject.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace M13.InterviewProject
{
    public class Startup
    {
        #region Private fields

        private IConfigurationRoot Configuration { get; }
        
        private const string BaseAppSettingsFileName = "appsettings";

        #endregion

        #region Ctor

        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"{BaseAppSettingsFileName}.json", false, true)
                .AddJsonFile($"{BaseAppSettingsFileName}.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        #endregion
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            var mapperConfiguration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new RuleProfile());
            });
            services.AddSingleton(mapperConfiguration.CreateMapper());
            
            services.Configure<AppSettings>(Configuration);
            services.AddHttpClients();

            services.AddTransient(typeof(IRepository<>), typeof(MemoryRepository<>));
            services.AddTransient<IRuleService, RuleService>();
            services.AddTransient<ISpellService, SpellService>();

            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
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
                app.UseSpaStaticFiles();
            }

            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../M13.ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
