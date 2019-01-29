namespace ProcessViewer.Api.Configuration.Abstract
{
    /// <summary>   Interface for notification service configuration. </summary>
    public interface INotificationServiceConfiguration
    {
        /// <summary>   Gets or sets the notification server port. </summary>
        ///
        /// <value> The notification server port. </value>

        int NotificationServerPort { get; set; }

        /// <summary>   Gets or sets the notification server keep alive period. </summary>
        ///
        /// <value> The notification server keep alive period. </value>

        int NotificationServerKeepAlivePeriod { get; set; }
    }
}