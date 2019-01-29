using ProcessViewer.Core.Adapters.Abstract;
using ProcessViewer.Core.Models;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace ProcessViewer.Core.Tests.Models
{
    public class ClientDecoratorTests
    {
        private Mock<IClientAdapter> _client;

        [SetUp]
        public void Setup()
        {
            _client = new Mock<IClientAdapter>();
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void ClientDecorator_Connected_ShouldReturnClientConnected(bool value)
        {
            _client.Setup(c => c.Connected).Returns(value);
            var clientDecorator = new ClientDecorator(_client.Object);

            Assert.That(clientDecorator.Connected, Is.EqualTo(value));
        }

        [Test]
        public void ClientDecorator_Handshake_CallGetStreamOnceIfNoBytesAvailable()
        {
            _client.Setup(c => c.Available).Returns(0);
            _client.Setup(c => c.GetStream()).Returns(new MemoryStream());
            var clientDecorator = new ClientDecorator(_client.Object);

            clientDecorator.Handshake();

            _client.Verify(c => c.Available, Times.Once);
            _client.Verify(c => c.GetStream(), Times.Exactly(1));
        }

        [Test]
        public void ClientDecorator_Handshake_CallGetStreamOnceIfNotGetFoundInIncomingStream()
        {
            var testBytes = Encoding.UTF8.GetBytes("Something");
            _client.Setup(c => c.Available).Returns(testBytes.Length);
            _client.Setup(c => c.GetStream()).Returns(new MemoryStream(testBytes));
            var clientDecorator = new ClientDecorator(_client.Object);

            clientDecorator.Handshake();

            _client.Verify(c => c.Available, Times.Once);
            _client.Verify(c => c.GetStream(), Times.Exactly(1));
        }

        [Test]
        public void ClientDecorator_Handshake_CallGetStreamTwiceIfGetFoundInIncomingStream()
        {
            var testBytes = Encoding.UTF8.GetBytes("GET");
            using (var testStream = new MemoryStream())
            {
                testStream.Write(testBytes, 0, testBytes.Length);
                testStream.Position = 0;
                _client.Setup(c => c.Available).Returns(testBytes.Length);
                _client.Setup(c => c.GetStream()).Returns(testStream);
                var clientDecorator = new ClientDecorator(_client.Object);

                clientDecorator.Handshake();
            }

            _client.Verify(c => c.Available, Times.Once);
            _client.Verify(c => c.GetStream(), Times.Exactly(2));
        }

        [Test]
        public void ClientDecorator_SendMessage_ShouldCallGetStreamOnce()
        {
            var message = "Test message";

            using (var testStream = new MemoryStream())
            {
                _client.Setup(c => c.GetStream()).Returns(testStream);
                var clientDecorator = new ClientDecorator(_client.Object);

                clientDecorator.SendMessage(message);
            }

            _client.Verify(c => c.GetStream(), Times.Exactly(1));
        }

        [Test]
        public void ClientDecorator_SendMessage_ShouldWriteTheMessageToStream()
        {
            var message = "Test message";
            byte[] responseBytes;
            using (var testStream = new MemoryStream())
            {
                _client.Setup(c => c.GetStream()).Returns(testStream);
                var clientDecorator = new ClientDecorator(_client.Object);


                clientDecorator.SendMessage(message);


                responseBytes = new byte[testStream.Position];
                testStream.Position = 0;
                testStream.Read(responseBytes);
            }

            var responseMessage = Encoding.UTF8.GetString(responseBytes);
            Assert.That(responseMessage, Does.Contain(message));
        }
    }
}