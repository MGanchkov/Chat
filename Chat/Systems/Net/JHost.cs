using System;
using System.Collections.Generic;
using System.Text;

namespace Systems.Net
{
    public class JHost
    {
        public JIP IP { get; set; }
        public int Port { get; set; }

        public Host Value() => TryParse(this, out Host ip) ? ip : Host.None(0);
        public static JHost Value(Host ip) => new JHost(ip);
        public static bool TryParse(JHost j, out Host host) => Host.TryParse(j, out host);

        /// <summary>
        /// Не использовать. Только для Json десерилизации.
        /// </summary>
        public JHost() { }
        private JHost(Host value) : this()
        {
            this.IP = JIP.Value(value.IP);
            this.Port = value.Port;
        }
    }
}
