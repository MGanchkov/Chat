using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Systems.Net
{
    public class CServer
    {
        public CID ID { get; private set; }
        public Host Host { get; private set; }
        public bool IsOpen { get; private set; }
        

        TcpListener Listener;


        #region Внутренние требования к реализации 
        //Классы должны быть максимально сохраняемые в Json формат, и при возможности загружаемые из них.
        // и/или реализовывать возможность загрузки из Json с учётом состояния всей системы в момент загрузки.
        public override string ToString() => $"Server ID={ID.Index}:{ID.Ticks}";
        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object obj) => (obj is CServer client) && client.ID == ID;

        public static bool operator ==(CServer a, CServer b) => (a is null && b is null) || (a is not null && b is not null && a.ID == b.ID);
        public static bool operator !=(CServer a, CServer b) => !(a == b);

        public JServer Json() => JServer.Value(this);
        public static bool TryParse(JServer j, out CServer sid)
        {
            sid = new CServer()
            {
                ID = j.ID.Value(),
                Host = j.Host.Value(),
            };
            return true;
        }
        #endregion
    }
}
