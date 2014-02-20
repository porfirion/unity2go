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

        private List<string> outputOrder = new List<string>();

        public Form1() {
            InitializeComponent();
        }

        private void log(string message) {
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
            if (socket == null) {
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
                        foreach(string message in outputOrder) {
                            Stream stream = new NetworkStream(socket);
                            MemoryStream tmpStream = new MemoryStream();
                            EndianBinaryWriter writer = new EndianBinaryWriter(EndianBitConverter.Big, tmpStream, Encoding.UTF8);
                            writer.Write((ushort)Encoding.UTF8.GetByteCount(message));
                            writer.Write(message.ToCharArray());
                            writer.Flush();
                            log(tmpStream.ToArray().ToString());
                            tmpStream.WriteTo(stream);
                            //byte[] bytes = stream.ToArray();
                            
                            /*byte[] buffer = Encoding.UTF8.GetBytes(message);

                            if (buffer.Length > 0) {
                                //byte[] lenBuffer = BitConverter.GetBytes((uint)buffer.Length);
                                byte[] lenBuffer = BitConverter.GetBytes((short)buffer.Length);
                                if (System.BitConverter.IsLittleEndian) {
                                    Array.Reverse(buffer);
                                    Array.Reverse(lenBuffer);
                                }
                                try {
                                    int sent = 0;

                                    sent = socket.Send(lenBuffer);
                                    sent += socket.Send(buffer, 0, 1, SocketFlags.None);
                                    sent += socket.Send(buffer, 1, buffer.Length - 1,SocketFlags.None);

                                    log("Sent " + sent.ToString() + " bytes");
                                }
                                catch (System.FormatException ex) {
                                    log("Exception: " + ex.Message);
                                }
                            }
                            else {
                                log("Empty message");
                            }*/
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
            outputOrder.Add(sendTextBox.Text);
        }
        private void sendButton_Click(object sender, EventArgs e) {
            sendMessage();
        }
    }
}
