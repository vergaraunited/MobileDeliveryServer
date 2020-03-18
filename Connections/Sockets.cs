using System;
using System.Collections;
using System.Collections.Generic;
using UMDGeneral.Interfaces.Interfaces;
using UMDGeneral.Utilities;

namespace MobileDeliveryServer.Connections
{
    public class Sockets : IEnumerable
    {
        List<IWebSocketConnection> allConnectedSockets = new List<IWebSocketConnection>();
        public Dictionary<Guid, ProcessMessages> dSocketMsgProc = new Dictionary<Guid, ProcessMessages>();

        public IEnumerator GetEnumerator()
        {
            foreach (var soc in allConnectedSockets)
            {
                // Yield each day of the week.
                yield return soc;
            }

        }
        public void AddSocketConnection(IWebSocketConnection socket)
        {
            if (!dSocketMsgProc.ContainsKey(socket.ConnectionInfo.Id))
                dSocketMsgProc.Add(socket.ConnectionInfo.Id, socket.ConnectionInfo.PM);
            else if (!allConnectedSockets.Contains(socket))
                throw new Exception("Socket Already connected!");

            if (!allConnectedSockets.Contains(socket))
                allConnectedSockets.Add(socket);
            else
                throw new Exception("Socket Already connected!");
        }

        public void RemoveSocketConnection(IWebSocketConnection socket)
        {
            if (dSocketMsgProc.ContainsKey(socket.ConnectionInfo.Id))
                dSocketMsgProc.Remove(socket.ConnectionInfo.Id);
            else if (allConnectedSockets.Contains(socket))
                throw new Exception("Socket Already connected!");

            if (allConnectedSockets.Contains(socket))
                allConnectedSockets.Remove(socket);
            else
                throw new Exception("Socket Not Found or Already disconnected!");
        }


        public IWebSocketConnection GetWebSocket(string id)
        {
            Guid gid;
            if (Guid.TryParse(id, out gid))
                return GetWebSocket(gid);
            else
                return null;
        }
        public IWebSocketConnection GetWebSocket(Guid id)
        {
            return allConnectedSockets.Find(a => a.ConnectionInfo.Id.CompareTo(id) == 0);
        }

        public bool SendAllSockets(string msg)
        {
            foreach (IWebSocketConnection s in allConnectedSockets)
                s.Send(msg);

            return true;
        }
        public bool SendAllSockets(byte [] msg)
        {
            foreach (IWebSocketConnection s in 
                allConnectedSockets)
                s.Send(msg);

            return true;
        }
    }
}
