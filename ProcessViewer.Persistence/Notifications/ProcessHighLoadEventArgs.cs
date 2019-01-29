using System;

namespace ProcessViewer.Persistence.Stores
{
    /// <summary>   Additional information for process high load events. </summary>
    ///
    /// <seealso cref="T:System.EventArgs"/>

    public class ProcessHighLoadEventArgs : EventArgs
    {
        /// <summary>   Gets or sets the message. </summary>
        ///
        /// <value> The message. </value>

        public string Message { get; set; }

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="message">  The message. </param>

        public ProcessHighLoadEventArgs(string message)
        {
            Message = message;
        }
    }
}