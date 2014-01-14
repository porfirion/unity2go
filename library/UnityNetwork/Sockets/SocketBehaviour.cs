using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace UnityNetwork.Sockets
{
    [AddComponentMenu("UnityNetwork/Sockets/SocketBehaviour")]
    public abstract class SocketBehaviour : NetworkBehaviour
    {
        protected byte[] data = new byte[65536];
        protected Socket socket;
        protected Thread list;
        protected EndPoint sender = (EndPoint)new IPEndPoint(IPAddress.Any, 0);
        protected IPEndPoint serverEP;
        protected IPEndPoint clientEP;
    }
}
