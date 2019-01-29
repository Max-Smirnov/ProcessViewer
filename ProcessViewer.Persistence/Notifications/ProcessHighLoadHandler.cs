using ProcessViewer.Persistence.Notifications.Abstract;
using ProcessViewer.Persistence.Stores;

namespace ProcessViewer.Persistence.Notifications
{
    /// <summary>   Process the high load handler. </summary>
    ///
    /// <param name="sender">   The sender. </param>
    /// <param name="args">     Process high load event information. </param>

    public delegate void ProcessHighLoadHandler(
        INotificationsProcessor sender, ProcessHighLoadEventArgs args);
}