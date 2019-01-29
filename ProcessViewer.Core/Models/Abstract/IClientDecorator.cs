using System;

namespace ProcessViewer.Core.Models.Abstract
{
    /// <summary>   Interface for client decorator. </summary>
    public interface IClientDecorator : IDisposable
    {
        /// <summary>Gets a value indicating whether the client is connected to a remote host.</summary>
        /// <returns>true if the client was connected to a remote resource as of the most recent operation; otherwise, false.</returns>

        bool Connected { get; }

        /// <summary>   Performs handshake with remote connection. </summary>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>

        bool Handshake();

        /// <summary>   Sends a message to the remote connection. </summary>
        ///
        /// <param name="message">  The message. </param>

        void SendMessage(string message);
    }
}