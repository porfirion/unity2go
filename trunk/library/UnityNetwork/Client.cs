using UnityEngine;

namespace UnityNetwork
{
    [AddComponentMenu("UnityNetwork/Client")]
    public class Client : NetworkBehaviour
    {
        public string ip = "localhost";
        public int port = 2010;

        protected override void InitLogManager()
        {
            LM = new LogManager("clientlog");
        }

        protected virtual void OnGUI()
        {
            if (Network.peerType == NetworkPeerType.Disconnected)
            {
                ip = GUILayout.TextField(ip);
                if (GUILayout.Button(new GUIContent("Connect")))
                {
                    Connect(ip, port);
                }
            }
            if (Network.peerType == NetworkPeerType.Client)
            {
                if (GUILayout.Button(new GUIContent("Disconnect")))
                {
                    Disconnect();
                }
            }
        }

        public void Connect(string ip, int port)
        {
            LM.Log("Connecting to " + ip + ":" + port);
            Network.Connect(ip, port);
        }
        public void Disconnect()
        {
            LM.Log("Disconnecting from server");
            Network.Disconnect();
        }

        protected virtual void OnConnectedToServer()
        {
            LM.Log("Connected to server");
        }

        protected virtual void OnDisconnectedFromServer()
        {
            LM.Log("Disconnected from server");
        }

        protected virtual void OnFailedToConnect(NetworkConnectionError error)
        {
            LM.Log("Could not connect to server: " + error);
        }

        public Object Inst(Object prefab, Vector3 position, Quaternion rotate)
        {
            LM.Log("Instantiate prefab " + prefab.name + " at position " + position);
            return Network.Instantiate(prefab, position, rotate, 0);
        }
    }
}
