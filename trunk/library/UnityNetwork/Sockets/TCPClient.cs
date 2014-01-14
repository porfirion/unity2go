using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace UnityNetwork.Sockets
{
    [AddComponentMenu("UnityNetwork/Sockets/TCPClient")]
    public class TCPClient : SocketBehaviour
    {
        protected StateClient state;
        protected string ip = "localhost";
        protected string message = "";
        protected int port = 2200;
        protected ASCIIEncoding encoding = new ASCIIEncoding();

        protected override void InitLogManager()
        {
            LM = new LogManager("clientTCPlog");
        }

        protected virtual void OnGUI()
        {
            GUILayout.Label(message);
            if(socket != null)
                GUILayout.Label("Socket connected: " + socket.Connected);
            if (state == StateClient.Disconnected)
            {
                ip = GUILayout.TextField(ip);
                port = Int32.Parse(GUILayout.TextField(port.ToString()));
                if (GUILayout.Button(new GUIContent("Connect")))
                {
                    Connect(ip, port);
                }
            }
            if (state == StateClient.Connected)
            {
                if (GUILayout.Button(new GUIContent("Disconnect")))
                {
                    Disconnect();
                }
            }
        }

        public void Connect(string ip, int port)
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                LM.Log("Connecting to " + ip + ":" + port);
                socket.Connect(ip, port);
                LM.Log("Connected");
                state = StateClient.Connected;
                socket.BeginReceive(data, 0, 65536, SocketFlags.None,
                    new AsyncCallback(GettedData), socket);
            }
            catch (Exception e)
            {
                LM.Log("Error connection: " + e.ToString());
                Disconnect();
            }
        }

        public void Disconnect()
        {
            LM.Log("Disconneting...");
            socket.Disconnect(true);
            socket.Close();
            LM.Log("Disconnected");
            state = StateClient.Disconnected;
        }

        public void SendToServer(byte[] sendData)
        {
            socket.Send(sendData);
        }
        protected void GettedData(IAsyncResult ar)
        {
            try
            {
                LM.Log("Getting data");
                Socket curSock = (Socket)ar.AsyncState;
                int bytesRead = curSock.EndReceive(ar);
                IPEndPoint rep = (IPEndPoint)curSock.RemoteEndPoint;

                if (bytesRead > 0)
                {
                    curSock.BeginReceive(data, 0, 65536, SocketFlags.None, new AsyncCallback(GettedData), socket);
                }
                message = "Server sayd: " + encoding.GetString(data);
                LM.Log(message);
            }
            catch (Exception e)
            {
                LM.Log("Error getting data: " + e.ToString());
                Disconnect();
            }
        }
    }
}
