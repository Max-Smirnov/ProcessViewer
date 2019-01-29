using ProcessViewer.Persistence.Stores;

namespace ProcessViewer.Persistence.Notifications.Abstract
{
    /// <summary>   Interface for notifications processor. </summary>
    public interface INotificationsProcessor
    {
        /// <summary>   Sends the notifications with the list of messages from processes which exceeded load limits. </summary>
        void SendNotifications();

        /// <summary>   Subscribes the given notificationHandler to high load notifications. </summary>
        ///
        /// <param name="notificationHandler">  The notification notificationHandler. </param>

        void Subscribe(ProcessHighLoadHandler notificationHandler);

        /// <summary>   Unsubscribes the given notification notificationHandler from high load notifications. </summary>
        ///
        /// <param name="notificationHandler">  The notification notificationHandler. </param>

        void Unsubscribe(ProcessHighLoadHandler notificationHandler);
    }
}