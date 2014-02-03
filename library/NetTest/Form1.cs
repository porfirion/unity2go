using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace NetTest {
    public partial class Form1 : Form {
        const int port = 25001;
        const string ip = "localhost";

        Socket socket = null;
        public Form1() {
            InitializeComponent();
        }

        private void checkStatus() {
            if (socket != null) {
                bool enableRead = socket.Poll(10, SelectMode.SelectRead);
                bool enableWrite = socket.Poll(10, SelectMode.SelectWrite);
                log.AppendText("Enable read: " + enableRead.ToString() + "\n");
                log.AppendText("Enable write: " + enableWrite.ToString() + "\n");
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ip, port);
            log.AppendText("Connected!\n");

            //socket.Blocking = false;
            log.AppendText("Blocking: " + socket.Blocking.ToString() + "\n");

            bool enableRead = socket.Poll(10, SelectMode.SelectRead);
            bool enableWrite = socket.Poll(10, SelectMode.SelectWrite);

            if (enableWrite) {
                byte[] msg = Encoding.UTF8.GetBytes("Hello world!");
                int bytesWritten = socket.Send(msg);
                log.AppendText("Written: " + bytesWritten.ToString() + "\n");
            }

            checkStatus();

            socket.Close();
        }
    }
}
