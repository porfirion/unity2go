using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace UnityNetwork.Sockets
{
    [AddComponentMenu("UnityNetwork/Sockets/UDPClient")]
    public class UDPClient : SocketBehaviour
    {
        public int chatLimit = 4;

        protected string ip = "127.0.0.1:2020";
        protected string message = "Type message here";
        protected string receiveText = " ";

        protected Queue<string> receive = new Queue<string>();
        protected Rect fRect = new Rect(10, 10, 150, 30),
                sRect = new Rect(10, 50, 150, 30),
                tRect = new Rect(10, 90, 150, 30),
                oRect = new Rect(10, 130, 150, 150);

        protected override void InitLogManager()
        {
            LM = new LogManager("clientUDPlog");
        }

        protected override void Awake()
        {
            base.Awake();
            list = new Thread(Listen);
            Init();
        }

        protected virtual void OnGUI()
        {
            ip = GUI.TextField(fRect, ip);
            message = GUI.TextField(sRect, message);
            if (GUI.Button(tRect, "Send"))
            {
                LM.Log("Sending message");
                Send(ip, message);
            }
            GUI.Label(oRect, receiveText);
        }

        protected void Init()
        {
            clientEP = new IPEndPoint(IPAddress.Any, 9090);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            LM.Log("Socket is created");
            socket.Bind((EndPoint)clientEP);
            LM.Log("Socket is bind");
            list.Start();
        }

        protected void Send(string ip, string temp)
        {
            serverEP = getEP(ip);
            socket.SendTo(System.Text.Encoding.Unicode.GetBytes(temp), SocketFlags.None, serverEP);
            LM.Log("Message is send. All OK");
        }

        protected IPEndPoint getEP(string text)
        {
            int port = Int32.Parse((text.Clone() as string).Remove(0, text.IndexOf(":") + 1));
            string host = text.Remove(text.IndexOf(":"), text.Length - text.IndexOf(":"));
            IPAddress hostIP = IPAddress.Parse(host);
            IPEndPoint endPoint = new IPEndPoint(hostIP, port);
            return endPoint;
        }

        protected void Listen()
        {
            while (true)
            {
                data = new byte[128];
                socket.ReceiveFrom(data, ref sender);
                NewMessage(System.Text.Encoding.Unicode.GetString(data));
            }
        }

        protected void NewMessage(string temp)
        {
            receiveText = "";
            if (receive.Count == chatLimit)
                receive.Dequeue();
            receive.Enqueue(temp);
            foreach (string text in receive)
            {
                receiveText += "\n" + text;
            }
        }

        protected void OnApplicationQuit()
        {
            socket.Close();
        }
    }
}
