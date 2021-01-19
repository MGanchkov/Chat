using System;
using System.Collections.Generic;
using System.Text;

namespace Systems
{
    public class CID
    {
        private static long SIndex;

        public long Index { get; }
        public ulong Ticks { get; }

        public DateTime DateTime { get; }

        public CID() : this((ulong)DateTime.Now.Ticks, System.Threading.Interlocked.Increment(ref SIndex)) { }
        private CID(ulong ticks, long index)
        {
            Ticks = ticks;
            Index = index;
            DateTime = new DateTime((long)ticks);
        }

        #region Внутренние требования к реализации 
        //Классы должны быть максимально сохраняемые в Json формат, и при возможности загружаемые из них.
        // и/или реализовывать возможность загрузки из Json с учётом состояния всей системы в момент загрузки.
        public override string ToString() =>
            $"ID {Index}: {DateTime.Year:0000}.{DateTime.Month:00}.{DateTime.Day:00} " +
                     $"{DateTime.Hour:00}:{DateTime.Minute:00}:{DateTime.Second:00}.{DateTime.Millisecond:000}";
        public override int GetHashCode() => Index.GetHashCode();
        public override bool Equals(object obj) => (obj is CID sid) && sid.Index == Index && sid.Ticks == Ticks;

        public static bool operator ==(CID a, CID b) => (a is null && b is null) || (a is not null && b is not null && a.Index == b.Index && a.Ticks == b.Ticks);
        public static bool operator !=(CID a, CID b) => !(a == b);

        public JID Json() => JID.Value(this);
        public static bool TryParse(JID j, out CID sid)
        {
            sid = new CID(j.Ticks, j.Index);
            return true;
        }
        #endregion
    }
}
