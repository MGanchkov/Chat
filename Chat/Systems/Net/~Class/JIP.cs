using System;
using System.Collections.Generic;
using System.Text;

namespace Systems.Net
{
    public class JIP
    {
        public byte A { get; set; }
        public byte B { get; set; }
        public byte C { get; set; }
        public byte D { get; set; }

        public IP Value() => TryParse(this, out IP ip) ? ip : IP.None;
        public static JIP Value(IP ip) => new JIP(ip);
        public static bool TryParse(JIP j, out IP sid) => IP.TryParse(j, out sid);

        /// <summary>
        /// Не использовать. Только для Json десерилизации.
        /// </summary>
        public JIP() { }
        private JIP(IP value) : this()
        {
            this.A = value.A;
            this.B = value.B;
            this.C = value.C;
            this.D = value.D;
        }
    }
}
