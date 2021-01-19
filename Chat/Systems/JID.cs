using System;
using System.Collections.Generic;
using System.Text;

namespace Systems
{
    public class JID
    {
        public long Index { get; set; }
        public ulong Ticks { get; set; }
        public DateTime DateTime { get; }

        public CID Value() => TryParse(this, out CID sid) ? sid : null;
        public static JID Value(CID sid) => new JID(sid);
        public static bool TryParse(JID j, out CID sid) => CID.TryParse(j, out sid);

        /// <summary>
        /// Не использовать. Только для Json десерилизации.
        /// </summary>
        public JID() { }
        private JID(CID value) : this()
        {
            this.Index = value.Index;
            this.Ticks = value.Ticks;
            this.DateTime = value.DateTime;
        }
    }
}
