using DotNet.gRPC.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.gRPC.Model.EventArgs
{
    public class MessageReceivedEventArgs
    {
        public string ClientId { get; set; }
        public Msg Message { get; set; }

        public MessageReceivedEventArgs(string clientId, Msg msg)
        {
            ClientId = clientId;
            Message = msg;
        }

    }
}
