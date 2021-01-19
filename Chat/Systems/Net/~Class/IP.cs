using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Systems.Net
{
    public struct IP
    {
        public byte A { get; }
        public byte B { get; }
        public byte C { get; }
        public byte D { get; }

        #region static объекты
        public static IP Any { get; } = new IP(0, 0, 0, 0);
        public static IP Local { get; } = new IP(127, 0, 0, 1);
        public static IP Loopback { get; } = new IP(127, 0, 0, 1);
        public static IP Broadcast { get; } = new IP(255, 255, 255, 255);
        public static IP None { get; } = new IP(255, 255, 255, 255);

        public static IP My
        {
            get
            {
                string localIP = Local.ToString();
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                        break;
                    }
                }
                return new IP(localIP);
            }
        }

        #endregion

        public static bool TryParse(string text, out IP ip)
        {
            string[] t = text.Split('.');
            if (t.Length == 4 && byte.TryParse(t[0], out byte a)
                              && byte.TryParse(t[1], out byte b)
                              && byte.TryParse(t[2], out byte c)
                              && byte.TryParse(t[3], out byte d))
            {
                ip = new IP(a, b, c, d);
                return true;
            }
            else
            {
                ip = Local;
                return false;
            }
        }
        public static bool TryParse(JIP j, out IP ip)
        {
            if (j != null)
            {
                ip = new IP(j.A, j.B, j.C, j.D);
                return true;
            }
            ip = IP.None;
            return false;
        }

        public override string ToString() => $"{A}.{B}.{C}.{D}";
        public override bool Equals(object obj)
        {
            if ((obj is IP ip) && (ip.A == A && ip.B == B && ip.C == C && ip.D == D)) return true;
            else return false;
        }
        public override int GetHashCode() => ((A * 256 + B) * 256 + C) * 256 + D;

        public static bool operator ==(IP a, IP b) => (a.A == b.A && a.B == b.B && a.C == b.C && a.D == b.D);
        public static bool operator !=(IP a, IP b) => (a.A != b.A || a.B != b.B || a.C != b.C || a.D != b.D);


        public static implicit operator System.Net.IPAddress(IP value)
        {
            if (value.A == 0 && value.B == 0 && value.C == 0 && value.D == 0) return IPAddress.Any;
            if (value.A == 127 && value.B == 0 && value.C == 0 && value.D == 1) return IPAddress.Loopback;
            if (value.A == 255 && value.B == 255 && value.C == 255 && value.D == 255) return IPAddress.Broadcast;
            if (value.A == 255 && value.B == 255 && value.C == 255 && value.D == 255) return IPAddress.None;
            return new IPAddress(new byte[] { value.A, value.B, value.C, value.D });
        }
        public static explicit operator IP(System.Net.IPAddress value)
        {
            byte[] bytes = value.GetAddressBytes();
            return new IP(bytes[0], bytes[1], bytes[2], bytes[3]);
        }

        public IP(byte a, byte b, byte c, byte d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }
        internal IP(string text)
        {
            string[] t = text.Split('.');
            if (t.Length == 4 && byte.TryParse(t[0], out byte a)
                              && byte.TryParse(t[1], out byte b)
                              && byte.TryParse(t[2], out byte c)
                              && byte.TryParse(t[3], out byte d))
            {
                A = a;
                B = b;
                C = c;
                D = d;
            }
            else throw new Exception("Нельзя распознать адрес.");
        }
    }
}
