using DotNet.gRPC.Common;
using DotNet.gRPC.Model;
using DotNet.gRPC.Protos;
using Grpc.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.gRPC.CommunicationHandler
{
    public class ServerCommunicateService : Communicate.CommunicateBase
    {
        // Concurrent dictionary to store connected client contexts
        private readonly ConcurrentDictionary<string, ClientContextModel> clients = new ConcurrentDictionary<string, ClientContextModel>();

        // Property to retrieve a list of connected client IDs
        public List<string> ConnectedClients
        {
            get
            {
                return clients.Keys.ToList();
            }
        }

        // Override method for handling bidirectional streaming communication
        public override async Task StreamSpeaker(IAsyncStreamReader<Msg> requestStream, IServerStreamWriter<Msg> responseStream, ServerCallContext context)
        {
            string clientId = Guid.NewGuid().ToString();

            try
            {
                // Create and manage a client context for the incoming connection
                var clientContext = new ClientContextModel(clientId, requestStream, responseStream, context);
                clients.TryAdd(clientId, clientContext);

                // Notify that a client has connected
                OnClientConnected(clientId);

                // Send an initial response message to the client
                await SendResponseMessage(clientId, clientId, "Internal");

                // Handle incoming messages asynchronously
                await HandleMessageAsync(clientId, clientContext.requestStream);

                // Remove the client context when the connection is closed
                clients.TryRemove(clientId, out _);

                // Notify that the client has disconnected
                OnClientDisconnected(clientId);
            }
            catch (Exception ex)
            {
                // Handle exceptions and notify when errors occur during client communication
                OnExceptionOccurred(clientId, ex, Constant.ConnectionToClientError);
            }
            finally
            {
                // Ensure the client context is removed even in case of exceptions
                clients.TryRemove(clientId, out _);
            }
        }

        // Override method for handling unidirectional communication
        public override Task<Msg> Speaker(Msg requestStream, ServerCallContext context)
        {
            try
            {
                // Notify when a message is received from a client
                OnMessageReceived(requestStream.ClientIdentity, requestStream);
            }
            catch (Exception ex)
            {
                // Handle exceptions and remove the client context on error
                clients.TryRemove(requestStream.ClientIdentity, out _);
                OnExceptionOccurred(requestStream.ClientIdentity, ex, Constant.ConnectionToClientError);
            }

            // Return an empty message response
            return Task.FromResult(new Msg { });
        }

        // Method to send a response message to a specific client
        public async Task SendResponseMessage(string clientId, string messageText, string serverMessageType)
        {
            var clientContext = clients[clientId];

            try
            {
                // Send an asynchronous response message to the client
                await SendAsync(clientContext.responseStream, new Msg { Message = messageText, ClientIdentity = clientContext.ClientId, ServerMessageType = serverMessageType });
            }
            catch (Exception ex)
            {
                // Handle exceptions and notify on send message errors
                OnExceptionOccurred(clientContext.ClientId, ex, Constant.SendMessageError);
            }
        }

        // Method to handle incoming messages asynchronously
        private async Task HandleMessageAsync(string clientId, IAsyncStreamReader<Msg> requestStream)
        {
            try
            {
                // Process incoming messages in a loop
                await foreach (var message in requestStream.ReadAllAsync())
                {
                    // Notify when a message is received from a client
                    OnMessageReceived(message.ClientIdentity, message);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and notify on connection errors
                OnExceptionOccurred(clientId, ex, Constant.ConnectionToClientError);
            }
        }

        // Method to send messages asynchronously
        private async Task SendAsync(IServerStreamWriter<Msg> responseStream, Msg message)
        {
            try
            {
                // Send a message asynchronously to the client
                await responseStream.WriteAsync(message);
            }
            catch (Exception ex)
            {
                // Handle exceptions and notify on send message errors
                OnExceptionOccurred(message.ClientIdentity, ex, Constant.SendMessageError);
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
