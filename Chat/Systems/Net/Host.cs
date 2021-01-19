using System;
using System.Collections.Generic;
using System.Text;

namespace Systems.Net
{
    public struct Host
    {
        public IP IP { get; }
        public int Port { get; }

        public static bool TryParse(string text, out Host host)
        {
            string[] t = text.Split(':');
            if ((t.Length == 2) && IP.TryParse(t[0], out IP ip) && int.TryParse(t[1], out int port))
            {
                host = new Host(ip, port);
                return true;
            }
            else
            {
                host = Host.None(0);
                return false;
            }
        }
        public static bool TryParse(JHost j, out Host host)
        {
            if (j != null)
            {
                host = new Host(j.IP.Value(), j.Port);
                return true;
            }
            host = Host.None(0);
            return false;
        }
        public override string ToString() => $"{IP}:{Port}";
        public override int GetHashCode() => IP.GetHashCode() * 65536 + Port;
        public override bool Equals(object obj)
        {
            if ((obj is Host host) && (host.IP.Equals(IP) && host.Port == Port)) return true;
            else return false;
        }

        public static bool operator ==(Host a, Host b) => a.Port == b.Port && a.IP == b.IP;
        public static bool operator !=(Host a, Host b) => a.Port != b.Port || a.IP != b.IP;

        public static implicit operator System.Net.IPAddress(Host value)
        {
            return value.IP;
        }
        public static implicit operator System.Net.IPEndPoint(Host value)
        {
            return new System.Net.IPEndPoint(value.IP, value.Port);
        }

        #region static объекты
        public static Host Any(int port) => new Host(0, 0, 0, 0, port);
        public static Host Local(int port) => new Host(127, 0, 0, 1, port);
        public static Host Loopback(int port) => new Host(127, 0, 0, 1, port);
        public static Host Broadcast(int port) => new Host(255, 255, 255, 255, port);
        public static Host None(int port) => new Host(255, 255, 255, 255, port);
        #endregion

        internal Host(byte a, byte b, byte c, byte d, int port) : this(new IP(a, b, c, d), port) { }
        public Host(IP ip, int port)
        {
            IP = ip;
            Port = port;
        }
    }
}
