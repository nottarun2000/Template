using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.gRPC.Model.EventArgs
{
    public class ErrorOccurredEventArgs
    {
        public string ClientId { get; set; }
        public Exception Exception { get; set; }
        public string ExceptionString { get; set; }


        public ErrorOccurredEventArgs(string clientId, Exception exception, string exceptionString)
        {
            ClientId = clientId;
            Exception = exception;
            ExceptionString = exceptionString;
        }

    }
}
