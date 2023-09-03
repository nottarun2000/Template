using DotNet.gRPC.Common;
using DotNet.gRPC.Protos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.gRPC.CommunicationHandler
{
    public class ClientCommunicateService
    {
        // Host and port configuration
        private string _port;
        private string _host;

        // gRPC streaming components
        private AsyncDuplexStreamingCall<Msg, Msg> stream { get; set; }
        private IAsyncStreamReader<Msg> responseStream { get; set; }
        private IClientStreamWriter<Msg> requestStream { get; set; }

        // Client ID property
        public string ClientId { get; set; } = "Not Assigned";

        // Default constructor
        public ClientCommunicateService()
        {
        }

        // Asynchronous method to handle incoming messages
        private async Task HandleMessageAsync()
        {
            try
            {
                await foreach (var message in responseStream.ReadAllAsync())
                {
                    // Update the client's ID if it matches the received message
                    if (message.Message == message.ClientIdentity)
                    {
                        ClientId = message.ClientIdentity;
                    }

                    // Notify when a message is received from the server
                    OnMessageReceived(message.ClientIdentity, message);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and notify on server shutdown
                OnExceptionOccurred(ClientId, ex, Constant.ServerShutDownMessage);
            }
        }

        // Method to establish a connection to the gRPC server
        internal void EstablishConnection(string host = "localhost", int port = 5000)
        {
            _host = host;
            _port = port.ToString();

            try
            {
                // Configure the target and channel for communication
                string targetSt = _host + ":" + _port;
                var channel = new Channel(targetSt, ChannelCredentials.Insecure);
                var client = new Communicate.CommunicateClient(channel);

                // Initialize streaming components for bidirectional communication
                stream = client.StreamSpeaker();
                requestStream = stream.RequestStream;
                responseStream = stream.ResponseStream;
            }
            catch (Exception ex)
            {
                // Handle exceptions and notify on connection errors
                OnExceptionOccurred(ClientId, ex, Constant.ConnectionToServerError);
            }
        }

        // Method to start bidirectional streaming with the server
        internal async Task StartStreaming()
        {
            try
            {
                // Notify that the client has connected
                OnClientConnected(ClientId);

                // Handle incoming messages asynchronously
                await HandleMessageAsync();

                // Notify that the client has disconnected
                OnClientDisconnected(ClientId);
            }
            catch (Exception ex)
            {
                // Handle exceptions and notify on connection errors
                OnExceptionOccurred(ClientId, ex, Constant.ConnectionToServerError);
            }
            finally
            {
                // Dispose of the streaming resources
                stream.Dispose();
            }
        }

        // Method to send a message to the server
        internal async Task SendMessageAsync(string messageText, string serverMessageType)
        {
            try
            {
                // Send a message asynchronously to the server
                await requestStream.WriteAsync(new Msg { Message = messageText, ClientIdentity = ClientId, ServerMessageType = serverMessageType });
            }
            catch (Exception ex)
            {
                // Handle exceptions and notify on send message errors
                OnExceptionOccurred(ClientId, ex, Constant.SendMessageError);
            }
        }

        // Methods for handling events (to be overridden by subclasses)
        internal virtual void OnClientConnected(string clientId)
        {
            throw new NotImplementedException();
        }

        internal virtual void OnMessageReceived(string clientId, Msg message)
        {
            throw new NotImplementedException();
        }

        internal virtual void OnClientDisconnected(string clientId)
        {
            throw new NotImplementedException();
        }

        internal virtual void OnExceptionOccurred(string clientId, Exception exception, string exceptionString)
        {
            throw new NotImplementedException();
        }
    }
}
