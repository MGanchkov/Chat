using System;
using System.Collections.Generic;
using System.Text;

namespace Systems.Net
{
    public class JServer
    {
        public JID ID { get; set; }
        public JHost Host { get; set; }


        public CServer Value() => TryParse(this, out CServer server) ? server : null;
        public static JServer Value(CServer server) => new JServer(server);
        public static bool TryParse(JServer j, out CServer server) => CServer.TryParse(j, out server);

        /// <summary>
        /// Не использовать. Только для Json десерилизации.
        /// </summary>
        public JServer() { }
        private JServer(CServer value) : this()
        {
            this.ID = JID.Value(value.ID);
            this.Host = JHost.Value(value.Host);

        }
    }
}
