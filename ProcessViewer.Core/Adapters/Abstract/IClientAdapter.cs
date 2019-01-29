using System;
using System.IO;
using System.Net.Sockets;

namespace ProcessViewer.Core.Adapters.Abstract
{
    /// <summary>   Interface for client adapter. </summary>
    public interface IClientAdapter : IDisposable
    {
        /// <summary>Gets the amount of data that has been received and is available to be read.</summary>
        /// <returns>The number of bytes of data received and available to be read.</returns>
        /// <exception cref="T:System.ObjectDisposedException">The client has been closed.</exception>

        int Available { get; }

        /// <summary>Gets a value indicating whether the client is connected to a remote host.</summary>
        /// <returns>true if the client was connected to a remote resource as of the most recent operation; otherwise, false.</returns>

        bool Connected { get; }

        /// <summary>Returns the <see cref="T:System.IO.Stream"></see> used to send and receive data.</summary>
        /// <returns>The underlying <see cref="System.IO.Stream"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">The client is not connected to a remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The client has been closed.</exception>

        Stream GetStream();
    }
}