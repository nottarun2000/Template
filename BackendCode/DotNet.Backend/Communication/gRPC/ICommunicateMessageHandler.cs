using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Backend.Communication.gRPC
{
    public interface ICommunicateMessageHandler
    {
        public object HandleMessage(string message);

    }
}
