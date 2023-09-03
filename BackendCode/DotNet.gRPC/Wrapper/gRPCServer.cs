using DotNet.gRPC.CommunicationHandler;
using DotNet.gRPC.Model.EventArgs;
using DotNet.gRPC.Protos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DotNet.gRPC.Common.Enum;

namespace DotNet.gRPC.Wrapper
{
    public class gRPCServer : ServerCommunicateService, ICommunicateService
    {
        // Private fields for server configuration
        private gRPCServer _service { get; set; }
        private Server _server { get; set; }
        private int _port { get; set; }
        private string _host { get; set; }

        // Events for client and message handling
        public event EventHandler<ClientEventArgs> ClientConnected;
        public event EventHandler<ClientEventArgs> ClientDisconnected;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ErrorOccurredEventArgs> ExceptionOccurred;

        // Constructor for initializing the gRPC server
        public gRPCServer(string host = "localhost", int port = 5000)
        {
            _host = host;
            _port = port;
            _service = this;
        }

        // Event handlers for client connected, disconnected, message received, and exceptions

        internal override void OnClientConnected(string clientId)
        {
            ClientConnected?.Invoke(this, new ClientEventArgs(clientId));
        }

        internal override void OnClientDisconnected(string clientId)
        {
            ClientDisconnected?.Invoke(this, new ClientEventArgs(clientId));
        }

        internal override void OnMessageReceived(string clientId, Msg message)
        {
            MessageReceived?.Invoke(this, new MessageReceivedEventArgs(clientId, message));
        }

        internal override void OnExceptionOccurred(string clientId, Exception exception, string exceptionString)
        {
            ExceptionOccurred?.Invoke(this, new ErrorOccurredEventArgs(clientId, exception, exceptionString));
        }

        // Method to start the gRPC server
        public async void StartServer()
        {
            _server = new Server
            {
                Services = { Communicate.BindService(_service) },
                Ports = { new ServerPort(_host, _port, ServerCredentials.Insecure) }
            };

            _server.Start();
        }
    }

}
