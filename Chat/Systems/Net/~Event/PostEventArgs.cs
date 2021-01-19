using System;
using System.Collections.Generic;
using System.Text;

namespace Systems.Net
{
    public class PostEventArgs : EventArgs
    {
        public TPost Type { get; }
        public string Text { get; }
        public byte[] Bytes { get; }

        public override string ToString() => $"Message {Type}";

        public PostEventArgs(byte[] bytes) : base()
        {
            Type = (TPost)bytes[1];
            switch (Type)
            {
                case TPost.Text:
                    Text = System.Text.Encoding.UTF8.GetString(bytes, 1, bytes.Length - 1);
                    Bytes = null;
                    break;
                case TPost.Bytes:
                    byte[] b = new byte[bytes.Length - 1];
                    Array.Copy(bytes, 1, b, 0, b.Length);
                    Text = null;
                    Bytes = b;
                    break;
                case TPost.Json:
                    Text = System.Text.Encoding.UTF8.GetString(bytes, 1, bytes.Length - 1);
                    Bytes = null;
                    break;
                default:
                    break;
            }
        }
    }
}
