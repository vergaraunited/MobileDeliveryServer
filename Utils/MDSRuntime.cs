using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MobileDeliveryServer.Utils
{
    internal static class MDSRuntime
    {
        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        public static bool IsRunningOnWindows()
        {
#if NET45 || NET40
            return true;
#else
            return (RuntimeInformation.IsOSPlatform(OSPlatform.Windows));
#endif
        }
    }

}
