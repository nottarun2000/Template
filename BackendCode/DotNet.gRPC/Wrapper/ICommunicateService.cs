using DotNet.gRPC.Model.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.gRPC.Wrapper
{
    public interface ICommunicateService
    {
        public event EventHandler<ClientEventArgs> ClientConnected;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public event EventHandler<ClientEventArgs> ClientDisconnected;

        public event EventHandler<ErrorOccurredEventArgs> ExceptionOccurred;

    }

}
