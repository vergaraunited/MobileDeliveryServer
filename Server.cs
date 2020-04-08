using MobileDeliveryLogger;
using MobileDeliveryServer.Comm;
using MobileDeliveryServer.Connections;
using System;
using System.Text;
using MobileDeliveryGeneral.Interfaces;
using MobileDeliveryGeneral.Interfaces.WebSocketServer;
using MobileDeliveryGeneral.Utilities;

namespace MobileDeliveryServer
{
    public class Server : isaUMDSocketServer
    {
        string url;
        string port;
        string name;
        LogLevel level; 
        public Server(string name, string url, string port="81", LogLevel loglevel=LogLevel.Info)
        {
            this.name = name;  this.url = url; this.port = port; level = loglevel;
        }
        public void Start(ProcessMsgDelegateRXRaw pmd)
        { 
            Sockets soks = new Sockets();
            Logger.AppName = name;

            Logger.Level = level;
            string port = this.port;
            WebSocketServer server;

            if ("localhost".CompareTo(url)==0)
                server = new WebSocketServer("ws://0.0.0.0" + ":" + port);
            else
                server = new WebSocketServer("ws://" + url + ":" + port);

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    try
                    { 
                        Logger.Info($"*****************  {name} - Client Connection Opened!  ************************* " + socket.ConnectionInfo.ClientPort);
                        socket.ConnectionInfo.PM = new ProcessMessages(pmd, socket.Send);
                        soks.AddSocketConnection(socket);
                        
                        socket.Send("opened - Port: " + socket.ConnectionInfo.ClientPort);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"{name} Server Receiving OnOpen Command Error: " + ex.Message + " - Port: " + socket.ConnectionInfo.ClientPort);
                    }
                };
                socket.OnClose = () =>
                {
                    try
                    { 
                        Logger.Info($"**************** {name} Server - Client Connection Closed!   ************************ - Port: " + socket.ConnectionInfo.ClientPort);
                        soks.RemoveSocketConnection(socket);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"{name} Server Receiving OnClose Command Error: " + ex.Message + " - Port: " + socket.ConnectionInfo.ClientPort);
                    }
                };
                socket.OnBinary = bmsg =>
                {
                    try
                    {
                        soks.dSocketMsgProc[socket.ConnectionInfo.Id].ProcessMessage(bmsg);
                        Logger.Debug($"{name} Server Receiving OnBinary Command: " + socket.ConnectionInfo.Id.ToString() + "- Port: " + socket.ConnectionInfo.ClientPort );
                    }
                    catch (Exception ex) {
                        //Logger.Error("Server Receiving OnBinary Command Error: " + ex.Message + "- Port: " + socket.ConnectionInfo.ClientPort);
                    }
                };
                socket.OnMessage = message =>
                {
                    try
                    { 
                        Logger.Debug($"{name} Server Receiving OnMessage: " + message + " - Port: " + socket.ConnectionInfo.ClientPort);
                        Logger.Debug($"{name} Server Relaying Messsage to all Clients: " + message);
                        soks.SendAllSockets($"{name} Server Message To All Clients: " + message);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"{name} Server Receiving OnMessage Command Error: " + ex.Message);
                    }
                };
                socket.OnPing = message =>
                {
                    try
                    { 
                        Logger.Debug($"{name} Server Got Pinged. Server Auto Pong Reply: " + socket.ConnectionInfo.Id + 
                            " - " + Encoding.UTF8.GetString(message, 0, message.Length) + 
                            " - Port: " + socket.ConnectionInfo.ClientPort);
                        byte[] p = new byte[2];
                        p[0] = 0;
                        p[1] = 1;
                        socket.SendPong(p);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"{name} Receiving OnPing from client - Command Error: " + ex.Message + " - Port: " + socket.ConnectionInfo.ClientPort);
                    }
                };
                socket.OnPong = message =>
                {
                    try
                    { 
                        Logger.Debug($"{name} Receiving Pong from client: " + socket.ConnectionInfo.Id +
                            " - " + Encoding.UTF8.GetString(message, 0, message.Length) + " - Port: " + socket.ConnectionInfo.ClientPort);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error($"{name} Receiving OnPong from client - Command Error: " + ex.Message + " - Port: " + socket.ConnectionInfo.ClientPort);
                    }
                };

            });

            //var input = Console.ReadLine();
            //while (input != "exit")
            //{
            //    Logger.Info("Broadcast: " + input);
            //    //isaCommand cmd = new MsgTypes.Command();
            //    //cmd.command = MsgTypes.eCommand.Broadcast;
            //    input = Console.ReadLine();
            //}
        }
    }
}
