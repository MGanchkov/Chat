using System;
using System.Collections.Generic;
using System.Text;

namespace Systems.Net
{
    public class JClient
    {
        public JID ID { get; set; }
        public JHost Host { get; private set; }
        public bool IsOpen { get; private set; }



        public CClient Value() => TryParse(this, out CClient client) ? client : null;
        public static JClient Value(CClient client) => new JClient(client);
        public static bool TryParse(JClient j, out CClient client) => CClient.TryParse(j, out client);

        /// <summary>
        /// Не использовать. Только для Json десерилизации.
        /// </summary>
        public JClient() { }
        private JClient(CClient value) : this()
        {
            ID = JID.Value(value.ID);
            Host = JHost.Value(value.Host);
        }
    }
}
