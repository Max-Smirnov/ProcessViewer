using ProcessViewer.Api.Configuration;
using ProcessViewer.Api.Configuration.Abstract;
using ProcessViewer.Api.Services;
using ProcessViewer.Core.Adapters;
using ProcessViewer.Core.Adapters.Abstract;
using ProcessViewer.Core.Models;
using ProcessViewer.Core.Models.Abstract;
using ProcessViewer.Persistence.Filters;
using ProcessViewer.Persistence.Filters.Abstract;
using ProcessViewer.Persistence.Notifications;
using ProcessViewer.Persistence.Notifications.Abstract;
using ProcessViewer.Persistence.Repositories;
using ProcessViewer.Persistence.Repositories.Abstract;
using ProcessViewer.Persistence.Stores;
using ProcessViewer.Persistence.Stores.Abstract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ProcessViewer.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<IProcessAdapter, ProcessAdapter>();
            services.AddTransient<IProcessModel, ProcessModel>();
            services.AddSingleton<IProcessesStore, ProcessesStore>();
            services.AddTransient<ILoadCheckerRunner, LoadCheckerRunner>();
            services.AddSingleton<INotificationsProcessor, NotificationsProcessor>();
            services.AddTransient<IRepository<IProcessModel>, ProcessModelsRepository>();
            services.AddTransient<ILoadChecker<IProcessModel>, ProcessCpuLoadChecker>(p=>new ProcessCpuLoadChecker(20));
            services.AddTransient<ILoadChecker<IProcessModel>, ProcessMemoryLoadChecker>(p=>new ProcessMemoryLoadChecker(200));
            services.AddSingleton<IListenerAdapter, TcpListenerAdapter>();
            services.AddTransient<IClientAdapter, TcpClientAdapter>();
            services.AddTransient<IClientDecorator, ClientDecorator>();
            services.AddSingleton<IClock, RealClock>();
            services.AddSingleton<IHostedService, ProcessRefresherService>();
            services.AddSingleton<IHostedService, NotificationsService>();
            var servicesConfiguration = Configuration.GetSection("servicesConfiguration").Get<ServicesConfiguration>();
            services.AddSingleton<INotificationServiceConfiguration>(servicesConfiguration);
            services.AddSingleton<IRefreshingServiceConfiguration>(servicesConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            loggerFactory.AddProvider(new DebugLoggerProvider());

            //app.UseHttpsRedirection();
            app.UseCors(o => o.AllowAnyOrigin());
            app.UseCors(o => o.AllowAnyHeader());
            app.UseCors(o => o.AllowAnyMethod());
            app.UseMvc();
        }
    }
}
