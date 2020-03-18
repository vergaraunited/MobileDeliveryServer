using System;
using System.Collections.Generic;
using System.Text;

namespace MobileDeliveryServer.Comm
{
    public enum FrameType : byte
    {
        Continuation,
        Text,
        Binary,
        Close = 8,
        Ping = 9,
        Pong = 10,
    }

}
