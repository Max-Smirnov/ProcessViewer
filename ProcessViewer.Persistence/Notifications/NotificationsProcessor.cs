using ProcessViewer.Persistence.Filters.Abstract;
using ProcessViewer.Persistence.Notifications.Abstract;
using ProcessViewer.Persistence.Stores;
using ProcessViewer.Persistence.Stores.Abstract;

namespace ProcessViewer.Persistence.Notifications
{
    /// <summary>   A notifications processor. </summary>
    ///
    /// <seealso cref="T:ProcessViewer.Persistence.Stores.Abstract.INotificationsProcessor"/>

    public class NotificationsProcessor : INotificationsProcessor
    {
        /// <summary>   Gets the processes store. </summary>
        ///
        /// <value> The processes store. </value>

        private IProcessesStore ProcessesStore { get; }

        /// <summary>   Event queue for all listeners interested in ProcessHighLoad events. </summary>
        private event ProcessHighLoadHandler ProcessHighLoad;

        /// <summary>   Gets the runner for load checkers. </summary>
        ///
        /// <value> The loadCheckerRunner. </value>

        private ILoadCheckerRunner LoadCheckerRunner { get; }

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="loadCheckerRunner">    The runner for load checkers. </param>
        /// <param name="processesStore">       The processes store. </param>

        public NotificationsProcessor(ILoadCheckerRunner loadCheckerRunner, IProcessesStore processesStore)
        {
            LoadCheckerRunner = loadCheckerRunner;
            ProcessesStore = processesStore;
        }

        /// <summary>   Sends the notifications with the list of messages from processes which exceeded load limits. </summary>
        ///
        /// <seealso cref="M:ProcessViewer.Persistence.Stores.Abstract.IProcessesStore.SendNotifications()"/>

        public void SendNotifications()
        {
            var messages = LoadCheckerRunner.GetNotifications(ProcessesStore.GetAll());
            if (messages == null)
            {
                return;
            }

            foreach (var notificationMessage in messages)
            {
                ProcessHighLoad?
                    .Invoke(this, new ProcessHighLoadEventArgs(
                        notificationMessage));
            }
        }

        /// <summary>   Subscribes the given notificationHandler. </summary>
        ///
        /// <param name="notificationHandler">  The notificationHandler. </param>

        public void Subscribe(ProcessHighLoadHandler notificationHandler)
        {
            ProcessHighLoad += notificationHandler;
        }

        /// <summary>   Unsubscribes the given notificationHandler. </summary>
        ///
        /// <param name="handler">  The notificationHandler. </param>

        public void Unsubscribe(ProcessHighLoadHandler handler)
        {
            ProcessHighLoad -= handler;
        }
    }
}