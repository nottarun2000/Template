using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.gRPC.Common
{
    public class Enum
    {
        public enum ServerMessageType
        { 
            Internal, 
            Network
        }

        public enum BroadCast
        { 
            BroadCast,
            NotBroadCast
        }

    }
}
