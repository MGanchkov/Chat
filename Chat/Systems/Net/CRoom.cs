using System;
using System.Collections.Generic;
using System.Text;

namespace Systems.Net
{
    public class CRoom
    {
        public CID ID { get; private set; }


        #region Внутренние требования к реализации 
        //Классы должны быть максимально сохраняемые в Json формат, и при возможности загружаемые из них.
        // и/или реализовывать возможность загрузки из Json с учётом состояния всей системы в момент загрузки.
        public override string ToString() => $"Room ID={ID.Index}:{ID.Ticks}";
        public override int GetHashCode() => ID.GetHashCode();
        public override bool Equals(object obj) => (obj is CRoom client) && client.ID == ID;

        public static bool operator ==(CRoom a, CRoom b) => (a is null && b is null) || (a is not null && b is not null && a.ID == b.ID);
        public static bool operator !=(CRoom a, CRoom b) => !(a == b);

        public JRoom Json() => JRoom.Value(this);
        public static bool TryParse(JRoom j, out CRoom sid)
        {
            sid = new CRoom()
            {
                ID = j.ID.Value(),
            };
            return true;
        }
        #endregion
    }

    public class JRoom
    {
        public JID ID { get; set; }



        public CRoom Value() => TryParse(this, out CRoom room) ? room : null;
        public static JRoom Value(CRoom room) => new JRoom(room);
        public static bool TryParse(JRoom j, out CRoom room) => CRoom.TryParse(j, out room);
        
        /// <summary>
        /// Не использовать. Только для Json десерилизации.
        /// </summary>
        public JRoom() { }
        private JRoom(CRoom value) : this()
        {
            ID = JID.Value(value.ID);

        }
    }
}
