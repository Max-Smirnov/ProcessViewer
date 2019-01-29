using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ProcessViewer.Core.Adapters.Abstract;
using ProcessViewer.Core.Models.Abstract;

namespace ProcessViewer.Core.Models
{
    /// <summary>   A client decorator. </summary>
    ///
    /// <seealso cref="T:ProcessViewer.Core.Models.Abstract.IClientDecorator"/>

    public class ClientDecorator : IClientDecorator
    {
        /// <summary>   Gets  the client. </summary>
        ///
        /// <value> The client. </value>

        private IClientAdapter Client { get; }

        /// <summary>   Constructor. </summary>
        ///
        /// <param name="client">   The client. </param>

        public ClientDecorator(IClientAdapter client)
        {
            Client = client;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
        /// resources.
        /// </summary>

        public void Dispose()
        {
            Client?.Dispose();
        }

        /// <summary>Gets a value indicating whether the client is connected to a remote host.</summary>
        /// <returns>true if the client was connected to a remote resource as of the most recent operation; otherwise, false.</returns>
        /// <seealso cref="P:ProcessViewer.Core.Models.Abstract.IClientDecorator.Connected"/>

        public bool Connected => Client.Connected;

        /// <summary>   Performs handshake with remote connection. </summary>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ///
        /// <seealso cref="M:ProcessViewer.Core.Models.Abstract.IClientDecorator.Handshake()"/>

        public bool Handshake()
        {
            var bytes = new Byte[Client.Available];

            Client.GetStream().Read(bytes, 0, bytes.Length);

            var data = Encoding.UTF8.GetString(bytes);

            if (Regex.IsMatch(data, "^GET"))
            {
                UpgradeToWebSocket(data);
                return true;
            }

            return false;
        }

        /// <summary>   Sends a message to the remote connection. </summary>
        ///
        /// <param name="message">  The message to send. </param>
        ///
        /// <seealso cref="M:ProcessViewer.Core.Models.Abstract.IClientDecorator.SendMessage(string)"/>

        public void SendMessage(string message)
        {
            int step = 125;

            Byte[] response = Encoding.UTF8.GetBytes(message);
            var stream = Client.GetStream();

            for (int i = 0; i < response.Length; i += step)
            {
                var data = response.Skip(i).Take(step).ToArray();
                byte startingByte = 0;
                if (i == 0) startingByte |= 0b00000001; // first text frame
                if ((i + 1) * step >= response.Length) startingByte |= 0b10000000; // last frame
                stream.Write(new byte[] { startingByte, (byte)data.Length }, 0, 2);
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
        }

        /// <summary>   Upgrade to web socket. </summary>
        ///
        /// <param name="data"> Data needed to compute hash. </param>

        private void UpgradeToWebSocket(string data)
        {
            const string eol = "\r\n"; // HTTP/1.1 defines the sequence CR LF as the end-of-line marker

            var response = Encoding.UTF8.GetBytes(
                "HTTP/1.1 101 Switching Protocols" + eol +
                "Connection: Upgrade" + eol +
                "Upgrade: websocket" + eol +
                "Sec-WebSocket-Accept: " +
                Convert.ToBase64String(
                    System.Security.Cryptography.SHA1.Create()
                        .ComputeHash(
                            Encoding.UTF8.GetBytes(
                                new Regex("Sec-WebSocket-Key: (.*)")
                                    .Match(data).Groups[1].Value.Trim() +
                                "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"))) +
                eol + eol);

            Client.GetStream().Write(response, 0, response.Length);
        }

    }
}