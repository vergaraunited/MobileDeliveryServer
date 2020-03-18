using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDeliveryServer.Comm
{
    public class SubProtocolNegotiationFailureException : Exception
    {
        public SubProtocolNegotiationFailureException() : base() { }

        public SubProtocolNegotiationFailureException(string message) : base(message) { }

        public SubProtocolNegotiationFailureException(string message, Exception innerException) : base(message, innerException) { }
    }

}
