using System.IO;
using System.Net.Sockets;
using ProcessViewer.Core.Adapters.Abstract;

namespace ProcessViewer.Core.Adapters
{
    /// <summary>   A TCP client adapter. </summary>
    ///
    /// <seealso cref="T:ProcessViewer.Core.Adapters.Abstract.IClientAdapter"/>

    public class TcpClientAdapter : IClientAdapter
    {
        /// <summary>   Gets the TCP client. </summary>
        ///
        /// <value> The TCP client. </value>

        private TcpClient TcpClient { get; }

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="tcpClient">    The TCP client. </param>

        public TcpClientAdapter(TcpClient tcpClient)
        {
            TcpClient = tcpClient;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
        /// resources.
        /// </summary>

        public void Dispose()
        {
            TcpClient?.Dispose();
        }

        /// <summary>Gets the amount of data that has been received from the network and is available to be read.</summary>
        /// <returns>The number of bytes of data received from the network and available to be read.</returns>
        /// <exception cref="T:System.Net.Sockets.SocketException">An error occurred when attempting to access the socket.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="System.Net.Sockets.Socket"></see> has been closed.</exception>
        /// <seealso cref="P:ProcessViewer.Core.Adapters.Abstract.IClientAdapter.Available"/>

        public int Available => TcpClient.Available;

        /// <summary>Gets a value indicating whether the underlying <see cref="T:System.Net.Sockets.Socket"></see> for a <see cref="T:System.Net.Sockets.TcpClient"></see> is connected to a remote host.</summary>
        /// <returns>true if the <see cref="System.Net.Sockets.TcpClient.Client"></see> socket was connected to a remote resource as of the most recent operation; otherwise, false.</returns>
        /// <seealso cref="P:ProcessViewer.Core.Adapters.Abstract.IClientAdapter.Connected"/>

        public bool Connected => TcpClient.Connected;

        /// <summary>Returns the <see cref="T:System.IO.Stream"></see> used to send and receive data.</summary>
        /// <returns>The underlying <see cref="System.IO.Stream"></see>.</returns>
        /// <exception cref="T:System.InvalidOperationException">The <see cref="System.Net.Sockets.TcpClient"></see> is not connected to a remote host.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="System.Net.Sockets.TcpClient"></see> has been closed.</exception>
        /// <seealso cref="M:ProcessViewer.Core.Adapters.Abstract.IClientAdapter.GetStream()"/>

        public Stream GetStream()
        {
            return TcpClient.GetStream();
        }
    }
}