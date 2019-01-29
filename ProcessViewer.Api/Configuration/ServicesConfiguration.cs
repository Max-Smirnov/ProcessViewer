using ProcessViewer.Api.Configuration.Abstract;

namespace ProcessViewer.Api.Configuration
{
    /// <summary>   The services configuration. </summary>
    ///
    /// <seealso cref="T:ProcessViewer.Api.Configuration.Abstract.INotificationServiceConfiguration"/>
    /// <seealso cref="T:ProcessViewer.Api.Configuration.Abstract.IRefreshingServiceConfiguration"/>

    public class ServicesConfiguration : INotificationServiceConfiguration,
        IRefreshingServiceConfiguration
    {
        /// <summary>   Gets or sets the notification server port. </summary>
        ///
        /// <value> The notification server port. </value>

        public int NotificationServerPort { get; set; }

        /// <summary>   Gets or sets the notification server keep alive period. </summary>
        ///
        /// <value> The notification server keep alive period. </value>

        public int NotificationServerKeepAlivePeriod { get; set; }

        /// <summary>   Gets or sets the refresh period of processes list. </summary>
        ///
        /// <value> The refresh period. </value>

        public int ProcessesListRefreshPeriod { get; set; }
    }
}