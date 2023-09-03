using DotNet.gRPC.Protos;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.gRPC.Model
{
    public class ClientContextModel
    {
        // Unique identifier for the client
        public string ClientId { get; }

        // Stream for reading client requests
        internal IAsyncStreamReader<Msg> requestStream;

        // Stream for writing server responses
        internal IServerStreamWriter<Msg> responseStream;

        // Server call context for handling the call
        internal readonly ServerCallContext context;

        // Constructor to initialize the ClientContextModel
        public ClientContextModel(string clientId, IAsyncStreamReader<Msg> requestStream, IServerStreamWriter<Msg> responseStream, ServerCallContext context)
        {
            // Assign the client's unique identifier
            ClientId = clientId;

            // Initialize the request and response streams
            this.requestStream = requestStream;
            this.responseStream = responseStream;

            // Store the server call context for handling the call
            this.context = context;
        }
    }

}
