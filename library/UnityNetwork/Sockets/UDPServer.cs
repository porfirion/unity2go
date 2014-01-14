using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace UnityNetwork.Sockets
{
    [AddComponentMenu("UnityNetwork/Sockets/UDPServer")]
    public class UDPServer : SocketBehaviour
    {
        protected int port = 2020;
        protected List<EndPoint> senders;

        protected StateServer state;

        protected override void InitLogManager()
        {
            LM = new LogManager("serverUDPlog");
        }

        protected override void Awake()
        {
            base.Awake();
            list = new Thread(Listen);
        }

        protected virtual void OnGUI()
        {
            if (state == StateServer.Off)
            {
                GUILayout.Label("Enter the port: ");
                port = int.Parse(GUILayout.TextField(port.ToString()));
                if (GUILayout.Button("Start Server"))
                {
                    StartHost(port);
                }
            }
            else if (state == StateServer.On)
            {
                if (GUILayout.Button("Stop Server"))
                {
                    StopHost();
                }
            }
        }

        protected virtual void StartHost(int port)
        {
            try
            {
                senders = new List<EndPoint>();

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                clientEP = new IPEndPoint(IPAddress.Any, 0);
                serverEP = new IPEndPoint(IPAddress.Any, port);
                LM.Log(sender.ToString());

                socket.Bind((EndPoint)serverEP);

                LM.Log("OK. Socket bind. Waiting for client request");

                list.Start();
            }
            catch (Exception e)
            {
                LM.Log("Init socket failed: " + e.ToString());
            }
            finally
            {
                state = StateServer.On;
            }

        }
        protected void Listen()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[128];
                    socket.ReceiveFrom(data, ref sender);
                    if (!senders.Contains(sender))
                    {
                        senders.Add(sender);
                    }
                    foreach (EndPoint temp in senders)
                    {
                        socket.SendTo(data, SocketFlags.None, temp);
                    }
                    LM.Log("someone from " + sender.ToString() + " said:");
                    LM.Log("     " + System.Text.Encoding.Unicode.GetString(data));
                }
                catch (Exception e)
                {
                    LM.Log("Socket listing failed: " + e.ToString());
                }
            }
        }

        protected virtual void StopHost()
        {
            list.Abort();
            socket.Close();
            LM.Log("Server shutdown");
            state = StateServer.Off;
        }

        protected void OnApplicationQuit()
        {
            StopHost();
        }
    }
}
