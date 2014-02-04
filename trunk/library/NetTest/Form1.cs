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

        private Socket socket = null;

        public Form1() {
            InitializeComponent();
        }

        private void log(string message) {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToLongTimeString());
            sb.Append(" ");
            sb.Append(DateTime.Now.Millisecond);
            sb.Append(":\t");
            sb.Append(message);
            sb.Append("\r\n");
            logBox.AppendText(sb.ToString());
            //logBox.AppendText(DateTime.Now.ToLongTimeString() + " " + DateTime.Now.Millisecond + ":\t" + message + "\r\n");
        }

        private void connectButton_Click(object sender, EventArgs e) {
            if (socket == null) {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            if (!socket.Connected) {
                socket.Connect(ip, port);
            }

            try {
                int n = socket.Send(Encoding.UTF8.GetBytes("Hello!"));
                log(n.ToString() + " bytes written");
            }
            catch (Exception ex) {
                log(ex.Message);
            }

            //socket.Close();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (socket == null || !socket.Connected) {
                connectButton.BackColor = Color.Red;
            }
            else {
                connectButton.BackColor = Color.Green;

                bool canRead = socket.Poll(100, SelectMode.SelectRead);
                bool canWrite = socket.Poll(100, SelectMode.SelectWrite);

                if (canRead) { 
                    readBox.Checked = true;

                    byte[] buffer = new byte[1000];
                    int n = socket.Receive(buffer);
                    string bufferS = Encoding.UTF8.GetString(buffer, 0, n);
                    bufferS.Trim();
                    log(n + " bytes read");
                    log(bufferS);
                } 
                else { 
                    readBox.Checked = false; 
                }
                if (canWrite) { writeBox.Checked = true; } else { writeBox.Checked = false; }
            }
        }
    }
}
