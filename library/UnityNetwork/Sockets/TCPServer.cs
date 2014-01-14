using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace UnityNetwork.Sockets
{
    [AddComponentMenu("UnityNetwork/Sockets/TCPServer")]
    public class TCPServer : SocketBehaviour
    {
        protected int port = 2200;

        protected StateServer state;

        protected override void InitLogManager()
        {
            LM = new LogManager("serverTCPlog");
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
            else if(state == StateServer.On)
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
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                clientEP = new IPEndPoint(IPAddress.Any, 0);
                serverEP = new IPEndPoint(IPAddress.Any, port);

                socket.Bind((EndPoint)serverEP);

                socket.Listen(1);
                socket.BeginAccept(new AsyncCallback(ReceiveCallback), socket);
                LM.Log("Wait connection… " + socket.LocalEndPoint);

            }
            catch (Exception e)
            {
                LM.Log("Error: " + e.ToString());
            }
            finally
            {
                state = StateServer.On;
            }
        }

        protected virtual void ReceiveCallback(IAsyncResult AsyncCall)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            data = encoding.GetBytes("I`m busy");

            Socket listener = (Socket)AsyncCall.AsyncState;
            Socket client = listener.EndAccept(AsyncCall);

            LM.Log("Client connect: " + client.RemoteEndPoint);
            client.Send(data);
            StopHost();

            listener.BeginAccept(new AsyncCallback(ReceiveCallback), listener);
        }

        protected virtual void StopHost()
        {
            //socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            state = StateServer.Off;
        }

        protected void OnApplicationQuit()
        {
            StopHost();
        }
    }
}
