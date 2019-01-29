using System;
using System.Net;
using System.Net.Sockets;
using ProcessViewer.Core.Adapters.Abstract;
using ProcessViewer.Core.Models;
using ProcessViewer.Core.Models.Abstract;

namespace ProcessViewer.Core.Adapters
{
    /// <summary>   A TCP listener adapter. </summary>
    ///
    /// <seealso cref="T:ProcessViewer.Core.Adapters.Abstract.IListenerAdapter"/>

    public class TcpListenerAdapter : IListenerAdapter
    {
        /// <summary>   The TCP listener. </summary>
        private TcpListener _tcpListener;

        /// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.TcpListener"></see> class starts listening for incoming connection requests on the specified port.</summary>
        /// <exception cref="T:System.Net.Sockets.SocketException">Use the <see cref="System.Net.Sockets.SocketException.ErrorCode"></see> property to obtain the specific error code. When you have obtained this code, you can refer to the Windows Sockets version 2 API error code documentation in MSDN for a detailed description of the error.</exception>
        /// <seealso cref="M:ProcessViewer.Core.Adapters.Abstract.IListenerAdapter.Start(int)"/>

        public void Start(int port)
        {
            _tcpListener = new TcpListener(IPAddress.Any, port);
            _tcpListener.Start();
        }

        /// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
        /// <param name="clientConnectedAsyncCallback">An <see cref="T:System.AsyncCallback"></see> delegate that references the method to invoke when the operation is complete.</param>
        /// <returns>An <see cref="System.IAsyncResult"></see> that references the asynchronous creation of the <see cref="System.Net.Sockets.TcpClient"></see>.</returns>
        /// <exception cref="T:System.Net.Sockets.SocketException">An error occurred while attempting to access the socket.</exception>
        /// <exception cref="T:System.ObjectDisposedException">The <see cref="System.Net.Sockets.Socket"></see> has been closed.</exception>
        /// <seealso cref="M:ProcessViewer.Core.Adapters.Abstract.IListenerAdapter.BeginAcceptConnection(AsyncCallback)"/>

        public void BeginAcceptConnection(AsyncCallback clientConnectedAsyncCallback)
        {
            _tcpListener.BeginAcceptTcpClient(clientConnectedAsyncCallback, this);
        }

        /// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:IClientDecorator"></see> to handle remote host communication.</summary>
        /// <param name="asyncResult">An <see cref="T:System.IAsyncResult"></see> returned by a call to the <see cref="M:System.Net.Sockets.TcpListener.BeginAcceptTcpClient(System.AsyncCallback,System.Object)"></see> method.</param>
        /// <returns>A <see cref="IClientDecorator"></see>.
        /// The <see cref="System.Net.Sockets.TcpClient"></see> used to send and receive data.</returns>
        /// <seealso cref="M:ProcessViewer.Core.Adapters.Abstract.IListenerAdapter.EndAcceptConnection(IAsyncResult)"/>

        public IClientDecorator EndAcceptConnection(IAsyncResult asyncResult)
        {
            var tcpClient = _tcpListener.EndAcceptTcpClient(asyncResult);
            var client = new ClientDecorator(new TcpClientAdapter(tcpClient));
            return client;
        }
    }
}