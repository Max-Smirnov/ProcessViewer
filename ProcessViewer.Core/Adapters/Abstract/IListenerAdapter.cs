using System;
using ProcessViewer.Core.Models.Abstract;

namespace ProcessViewer.Core.Adapters.Abstract
{
    /// <summary>   Interface for listener adapter. </summary>
    public interface IListenerAdapter
    {
        /// <summary>Instantiates underlying listener and starts listening for incoming connection requests on the specified port.</summary>
        ///
        /// <param name="port"> The port. </param>

        void Start(int port);

        /// <summary>Begins an asynchronous operation to accept an incoming connection attempt.</summary>
        /// <param name="clientConnectedAsyncCallback">An <see cref="T:System.AsyncCallback"></see> delegate that references the method to invoke when the operation is complete.</param>

        void BeginAcceptConnection(AsyncCallback clientConnectedAsyncCallback);

        /// <summary>Asynchronously accepts an incoming connection attempt and creates a new <see cref="T:IClientDecorator"></see> to handle remote host communication.</summary>
        /// <param name="asyncResult">An <see cref="T:System.IAsyncResult"></see> returned by a call to the <see cref="M:System.Net.Sockets.TcpListener.BeginAcceptTcpClient(System.AsyncCallback,System.Object)"></see> method.</param>
        /// <returns>A <see cref="IClientDecorator"></see>.
        /// The <see cref="IClientAdapter"></see> used to send and receive data.</returns>

        IClientDecorator EndAcceptConnection(IAsyncResult asyncResult);
    }
}
