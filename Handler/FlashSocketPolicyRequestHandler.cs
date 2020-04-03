﻿using MobileDeliveryLogger;
using MobileDeliveryServer.Comm;
using System.Text;
using MobileDeliveryGeneral.Interfaces.Interfaces;

namespace MobileDeliveryServer.Handler
{
    public class FlashSocketPolicyRequestHandler
    {
        public static string PolicyResponse =
"<?xml version=\"1.0\"?>\n" +
"<cross-domain-policy>\n" +
"   <allow-access-from domain=\"*\" to-ports=\"*\"/>\n" +
"   <site-control permitted-cross-domain-policies=\"all\"/>\n" +
"</cross-domain-policy>\n" +
"\0";

        public static IHandler Create(WebSocketHttpRequest request)
        {
            return new ComposableHandler
            {
                Handshake = sub => FlashSocketPolicyRequestHandler.Handshake(request, sub),
            };
        }

        public static byte[] Handshake(WebSocketHttpRequest request, string subProtocol)
        {
            Logger.Debug("Building Flash Socket Policy Response");
            return Encoding.UTF8.GetBytes(PolicyResponse);
        }
    }

}
