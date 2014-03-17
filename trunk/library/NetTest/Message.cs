using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTest {
    abstract class Message {
        public static Dictionary<string, int>  typesCodes = new Dictionary<string, int>() {
            {"TextMessage", 1}
        };
        public abstract byte[] ToBytes();

        public int GetTypeCode() {
            return Message.typesCodes[this.GetType().Name];
        }

        public static byte[] ConvertValueToBytes(object message) {
            byte[] res;
            if (message is string) {
                List<byte> list = new List<byte>();
                string str = (string)message;
                UInt16 len = (UInt16)Encoding.UTF8.GetByteCount(str);
                list.AddRange(ConvertValueToBytes(len));
                list.AddRange(Encoding.UTF8.GetBytes(str));

                res = (byte[])list.ToArray();
            }
            else {
                if (message is ushort)
                    res = BitConverter.GetBytes((ushort)message);
                else if (message is uint)
                    res = BitConverter.GetBytes((uint)message);
                else if (message is ulong)
                    res = BitConverter.GetBytes((ulong)message);
                else if (message is short)
                    res = BitConverter.GetBytes((short)message);
                else if (message is int)
                    res = BitConverter.GetBytes((int)message);
                else if (message is long)
                    res = BitConverter.GetBytes((long)message);
                else if (message is float)
                    res = BitConverter.GetBytes((float)message);
                else if (message is double)
                    res = BitConverter.GetBytes((double)message);
                else {
                    throw new InvalidCastException("Can't convert to bytes value of type " + message.GetType().Name);
                }

                if (BitConverter.IsLittleEndian) {
                    res = (byte[])res.Reverse().ToArray();
                }
            }

            return res;
        }
    }
}
