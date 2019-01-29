using ProcessViewer.Api.Configuration.Abstract;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using ProcessViewer.Persistence.Notifications.Abstract;
using ProcessViewer.Persistence.Stores.Abstract;
using Microsoft.Extensions.Logging;

namespace ProcessViewer.Api.Services
{
    /// <summary>   A service for refreshing processes list. </summary>
    ///
    /// <seealso cref="T:Microsoft.Extensions.Hosting.BackgroundService"/>

    public class ProcessRefresherService : BackgroundService
    {
        /// <summary>   Gets the processes store. </summary>
        ///
        /// <value> The processes store. </value>

        private IProcessesStore ProcessesStore { get; }

        /// <summary>   Gets the processes monitor. </summary>
        ///
        /// <value> The processes monitor. </value>

        public INotificationsProcessor NotificationsProcessor { get; }

        /// <summary>   Gets the refreshing service configuration. </summary>
        ///
        /// <value> The configuration. </value>

        private IRefreshingServiceConfiguration Configuration { get; }

        /// <summary>   Gets the logger. </summary>
        ///
        /// <value> The logger. </value>

        private ILogger Logger { get; }

        ///  <summary>   Constructor. </summary>
        /// 
        ///  <param name="processesStore">   The processes store. </param>
        /// <param name="notificationsProcessor">   The processes monitor. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        ///  <param name="configuration">    The service configuration. </param>
        public ProcessRefresherService(IProcessesStore processesStore, INotificationsProcessor notificationsProcessor, 
            ILoggerFactory loggerFactory, IRefreshingServiceConfiguration configuration)
        {
            ProcessesStore = processesStore;
            NotificationsProcessor = notificationsProcessor;
            Configuration = configuration;
            Logger = loggerFactory.CreateLogger("RequestInfoLogger"); ;
        }

        /// <summary>
        /// This method is called when the <see cref="T:Microsoft.Extensions.Hosting.IHostedService" />
        /// starts. The implementation should return a task that represents the lifetime of the long
        /// running operation(s) being performed.
        /// </summary>
        ///
        /// <param name="stoppingToken">    Triggered when
        ///                                 <see cref="M:Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken)" />
        ///                                 is called. </param>
        ///
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that represents the long running operations.
        /// </returns>
        ///
        /// <seealso cref="M:Microsoft.Extensions.Hosting.BackgroundService.ExecuteAsync(CancellationToken)"/>

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.Log(LogLevel.Trace, "ProcessRefresherService:ExecuteAsync Service Starting");

            do
            {
                ProcessesStore.Refresh();
                NotificationsProcessor.SendNotifications();

                await Task.Delay(Configuration.ProcessesListRefreshPeriod, stoppingToken);
            }
            while (!stoppingToken.IsCancellationRequested);

            Logger.Log(LogLevel.Trace, "ProcessRefresherService:ExecuteAsync Service Stopping");
        }
    }
}
