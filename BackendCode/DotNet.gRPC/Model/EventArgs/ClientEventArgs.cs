using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.gRPC.Model.EventArgs
{
    public class ClientEventArgs
    {
        public string ClientId { get; set; }

        public ClientEventArgs(string clientId)
        {
            clientId = clientId;
        }

    }
}
