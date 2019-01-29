using ProcessViewer.Api.Configuration.Abstract;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using ProcessViewer.Core.Adapters.Abstract;
using ProcessViewer.Persistence.Notifications.Abstract;
using ProcessViewer.Persistence.Stores;
using Microsoft.Extensions.Logging;

namespace ProcessViewer.Api.Services
{
    /// <summary>   A service for accessing notifications information. </summary>

    public class NotificationsService : BackgroundService
    {
        /// <summary>   Gets the processes monitor. </summary>
        ///
        /// <value> The processes monitor. </value>

        private INotificationsProcessor ProcessesesMonitor { get; }

        /// <summary>   Gets the logger. </summary>
        ///
        /// <value> The logger. </value>

        private ILogger Logger { get; }

        /// <summary>   Gets the configuration. </summary>
        ///
        /// <value> The configuration. </value>

        private INotificationServiceConfiguration Configuration { get; }

        /// <summary>   Gets the listener adapter. </summary>
        ///
        /// <value> The listener adapter. </value>

        private IListenerAdapter ListenerAdapter { get; }

        /// <summary>   Gets or sets the cancellation token. </summary>
        ///
        /// <value> The cancellation token. </value>

        private CancellationToken CancellationToken { get; set; }

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="processesesMonitor">   The processes monitor. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <param name="configuration">    The configuration. </param>
        /// <param name="listenerAdapter">  The listener adapter. </param>

        public NotificationsService(INotificationsProcessor processesesMonitor, ILoggerFactory loggerFactory,
            INotificationServiceConfiguration configuration, IListenerAdapter listenerAdapter)
        {
            ProcessesesMonitor = processesesMonitor;
            Logger = loggerFactory.CreateLogger("TestLogger");
            Configuration = configuration;
            ListenerAdapter = listenerAdapter;
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CancellationToken = stoppingToken;
            Logger.Log(LogLevel.Trace, "NotificationsService:ExecuteAsync Service Starting");
            ListenerAdapter.Start(Configuration.NotificationServerPort);
            Logger.Log(LogLevel.Information, "NotificationsService:ExecuteAsync Service Started");
            ListenerAdapter.BeginAcceptConnection(ClientConnectedAsyncCallback);
            Logger.Log(LogLevel.Trace, "NotificationsService:ExecuteAsync Waiting for new client to connect");
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(Configuration.NotificationServerKeepAlivePeriod, stoppingToken);
            }
            Logger.Log(LogLevel.Trace, "NotificationsService:ExecuteAsync Service Stopping");
        }

        /// <summary>
        /// Async callback, called on completion of client connected Asynchronous callback.
        /// </summary>
        ///
        /// <param name="result">   The result of the asynchronous operation. </param>

        private void ClientConnectedAsyncCallback(IAsyncResult result)
        {
            Logger.Log(LogLevel.Information, "NotificationsService:ClientConnectedAsyncCallback Client Connected");
            var listener = (IListenerAdapter)result.AsyncState;
            using (var client = listener.EndAcceptConnection(result))
            {
                listener.BeginAcceptConnection(ClientConnectedAsyncCallback);
                Logger.Log(LogLevel.Trace, "NotificationsService:ClientConnectedAsyncCallback Waiting for new client to connect");

                if (!client.Handshake())
                {
                    Logger.Log(LogLevel.Trace, "NotificationsService:ClientConnectedAsyncCallback Client doesn't support connection upgrade, disconnecting");
                    return;
                }

                Logger.Log(LogLevel.Trace, "NotificationsService:ClientConnectedAsyncCallback Connection Upgraded");

                ProcessesesMonitor.Subscribe(NotificationHandler);

                while (!CancellationToken.IsCancellationRequested) // keep connection alive
                {
                    if (!client.Connected)
                    {
                        break;
                    }
                    Thread.Sleep(Configuration.NotificationServerKeepAlivePeriod);
                }

                ProcessesesMonitor.Unsubscribe(NotificationHandler);

                void NotificationHandler(INotificationsProcessor sender, ProcessHighLoadEventArgs args)
                {
                    try
                    {
                        Logger.Log(LogLevel.Trace,
                            "NotificationsService:ClientConnectedAsyncCallback:NotificationHandler " +
                            "Message Arrived, Sending Notification");
                        client.SendMessage(args.Message);
                        Logger.Log(LogLevel.Trace, "NotificationsService:ClientConnectedAsyncCallback:NotificationHandler Message sent");
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(LogLevel.Error, ex,
                            "NotificationsService:ClientConnectedAsyncCallback:NotificationHandler " +
                            "Couldn't send notification, disconnecting", args.Message);
                    }
                }
            }
            Logger.Log(LogLevel.Trace, "NotificationsService:ClientConnectedAsyncCallback Client disconnected");
        }
    }
}
