using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace NetTest {
    class TextMessage: Message {
        public string text = "";

        public TextMessage(string text) {
            this.text = text;
        }

        public override byte[] ToBytes() {
            List<byte> res = new List<byte>();

            res.AddRange(TextMessage.ConvertValueToBytes((UInt32)this.GetTypeCode()));
            res.AddRange(TextMessage.ConvertValueToBytes(this.text));

            res.InsertRange(0, TextMessage.ConvertValueToBytes((UInt32)res.Count));
            
            return res.ToArray();
        }
    }
}
