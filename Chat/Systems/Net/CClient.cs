using System;
using System.Collections.Generic;
using System.Text;

namespace Systems.Net
{
    public class CClient
    {
        public CID ID { get; private set; }
        public Host Host { get; private set; }
        public bool IsOpen { get; private set; }



        #region Внутренние требования к реализации 
        //Классы должны быть максимально сохраняемые в Json формат, и при возможности загружаемые из них.
        // и/или реализовывать возможность загрузки из Json с учётом состояния всей системы в момент загрузки.
        public override string ToString() => $"Client ID={ID.Index}:{ID.Ticks}";
        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object obj) => (obj is CClient client) && client.ID == ID;

        public static bool operator ==(CClient a, CClient b) => (a is null && b is null) || (a is not null && b is not null && a.ID == b.ID);
        public static bool operator !=(CClient a, CClient b) => !(a == b);

        public JClient Json() => JClient.Value(this);
        public static bool TryParse(JClient j, out CClient sid)
        {
            sid = new CClient()
            {
                ID = j.ID.Value(),
                Host = j.Host.Value(),
            };
            return true;
        }
        #endregion
    }
}
