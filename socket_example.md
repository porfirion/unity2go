
```
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkConnection : MonoBehaviour
{
    public string server_IPAdress = "127.0.0.1";
    public int server_Port = 3333;
    byte[] bytes = new byte[1024];
    public Socket m_Socket;
    private byte[] Data = new byte[2048];
    private DateTime _lastDataTime;
    private StringBuilder Buffer= new StringBuilder();
    void Start()
    {
        Connect();
    }
    /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Connect()
    {
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(server_IPAdress), server_Port);
        m_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            m_Socket.Connect(ipep);
        }
        catch // (SocketException e)
        {
            Debug.Log("NO Connect");
            return;
        }
        WaitForData();
    }
    //отправляем сообщение на сервер
    public void Send(string msg)
    {
        m_Socket.Send(System.Text.Encoding.ASCII.GetBytes(msg+"\0"));
    }
    void OnDataReceived(IAsyncResult asyn)
    {
        int bytesReceived = 0;
        try
        {
            if (m_Socket.Connected)
            {
                bytesReceived = m_Socket.EndReceive(asyn);
            }
        }
        catch (SocketException)
        {
            m_Socket.Close();
        }

        if (bytesReceived == 0)
        {
           // Disconnect("bytesReceived=0");
            return;
        }

        _lastDataTime = DateTime.Now;

        #region Parse received data to requests

        ////try
        //{
        char[] chars = new char[bytesReceived];

        //Send("<out/>");
        int charLen = Encoding.UTF8.GetChars(Data, 0, bytesReceived, chars, 0);
        string str = new String(chars);

        print("IN:" + str);
        //if (Buffer.Length + str.Length > Settings.MAX_REQUEST_LENGTH)
        //{
        //    // throw new ClientDataError(this, "Получен слишком большой пакет данных >{0}", Settings.MAX_REQUEST_LENGTH);
        //}


        string dataStr;

        foreach (char c in str)
        {
            if (c == '\0')
            {
                if (Buffer.Length > 0)
                {
                    dataStr = Buffer.ToString();
                    Buffer.Length = 0;

                    //check for ping
                    if (dataStr.Length == 7 && dataStr.Equals("<PING/>"))
                    {
                        //Server.WriteDebug("PING");
                        continue;
                    }

                    OnRequest(dataStr);
                }
            }
            else
            {
                Buffer.Append(c);
            }
        }
        //}
        //catch (Exception cde)
        //{
        //    Server.WriteError(cde, "Failed to parse received data.");
        //    Disconnect("Failed to parse received data: " + cde.Message);
        //    return;
        //}
        #endregion

        WaitForData();
    }
    //получили запрос от сервера
    private void OnRequest(string request)
    {
        print(request);
    }
    private void WaitForData()
    {
        try
        {
            m_Socket.BeginReceive(
                    Data,
                    0,
                    Data.Length,
                    SocketFlags.None,
                    new AsyncCallback(OnDataReceived),
                    null);
        }
        catch (ObjectDisposedException)
        {
            //Trace.WriteLine( "Socket has been already disposed");
        }
        catch (SocketException se)
        {
            //Trace.WriteLine(se, "Failed to wait for data");
        }
    }
    void OnApplicationQuit()
    {
        m_Socket.Close();
    }
}
```