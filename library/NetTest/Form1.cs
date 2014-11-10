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
using System.IO;
using MiscUtil.IO;
using MiscUtil.Conversion;

namespace NetTest {
    public partial class Form1 : Form {
        const int port = 25001;
        const string ip = "localhost";

        private Socket socket = null;

        private List<Message> outputOrder = new List<Message>();

        public Form1() {
            InitializeComponent();
        }

        public void log(string message) {
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.ToLongTimeString());
            sb.Append(" ");
            sb.Append(DateTime.Now.Millisecond);
            sb.Append(":\t");
            sb.Append(message + "\n");
            sb.Append("\n");
            logBox.AppendText(sb.ToString() + "\r\n");
            //logBox.AppendText(DateTime.Now.ToLongTimeString() + " " + DateTime.Now.Millisecond + ":\t" + message + "\r\n");
        }

        private void connectButton_Click(object sender, EventArgs e) {
            if (socket == null || !socket.Connected) {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            
            if (!socket.Connected) {
                socket.Connect(ip, port);
            }

            try {
                //int n = socket.Send(Encoding.UTF8.GetBytes("Hello!"));
                //log(n.ToString() + " bytes written");
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

                    try {
                        byte[] buffer = new byte[1000];
                        int n = socket.Receive(buffer);
                        string bufferS = Encoding.UTF8.GetString(buffer, 0, n);
                        bufferS.Trim();
                        log(n + " bytes read");
                        log(bufferS);
                    }
                    catch (Exception ex) {
                        log("Error receiving: " + ex.Message);
                    }
                } 
                else { 
                    readBox.Checked = false; 
                }
                if (canWrite) { 
                    writeBox.Checked = true;
                    if (outputOrder.Count > 0) {
                        Stream stream = new NetworkStream(socket);
                        foreach(Message message in outputOrder) {
                            if (message is TextMessage) {
                                byte[] bytes = message.ToBytes();
                                log(formatBytes(bytes));
                                //log("Converter    : " + formatBytes(bytes));
                                stream.Write(bytes, 0, bytes.Length);
                            }
                        }
                        outputOrder.Clear();
                    }
                } 
                else { 
                    writeBox.Checked = false; 
                }
            }
        }

        private void sendMessage() {
            outputOrder.Add(new TextMessage(sendTextBox.Text));
        }
        private void sendButton_Click(object sender, EventArgs e) {
            sendMessage();
        }

        public static string formatBytes(Byte[] bytes) {
            string value = "";
            foreach (Byte b in bytes)
                value += String.Format("{0:X2} ", b);

            return value;
        }
    }
}
