using UnityEngine;

namespace UnityNetwork
{
    [AddComponentMenu("UnityNetwork/Server")]
    public class Server : NetworkBehaviour
    {
        public int port = 2010;
        public int limitPlayers = 2;

        protected override void InitLogManager()
        {
            LM = new LogManager("serverlog");
        }

        protected virtual void OnGUI()
        {
            if (Network.peerType == NetworkPeerType.Disconnected)
            {
                if (GUILayout.Button(new GUIContent("Start Server")))
                {
                    StartHost(limitPlayers, port, true, true);
                }
            }
            if (Network.peerType == NetworkPeerType.Server)
            {
                if (GUILayout.Button(new GUIContent("Stop Server")))
                {
                    StopHost();
                }
            }
        }

        public void StartHost(int players, int port, bool useNat, bool initializeSecurity)
        {
            LM.Log("Initialize Server: players " + players + " port " + port + " NAT " + useNat + " initialize security " + initializeSecurity);
            if(initializeSecurity)
                Network.InitializeSecurity();
            Network.InitializeServer(players, port, useNat);
        }

        public void StopHost()
        {
            LM.Log("Shutdown Server");
            Network.Disconnect();
        }

        protected virtual void OnServerInitialized()
        {
            LM.Log("Server initialized");
        }

        protected virtual void OnPlayerConnected(NetworkPlayer player)
        {
            LM.Log("Player connected: ip " + player.ipAddress + " port " + player.port);
        }

        protected virtual void OnPlayerDisconnected(NetworkPlayer player)
        {
            LM.Log("Player disconnected: ip " + player.ipAddress + " port " + player.port);
            Network.RemoveRPCs(player);
            Network.DestroyPlayerObjects(player);
        }
        
        protected void CloseConnection(NetworkPlayer player, bool sendDisconnectionNotification)
        {
            LM.Log("Disconnecting: ip " + player.ipAddress + " port " + player.port);
            Network.CloseConnection(player, sendDisconnectionNotification);
        }

        protected int Ping(NetworkPlayer player)
        {
            return Network.GetAveragePing(player);
        }

        protected void PingAllLog()
        {
            foreach (var temp in Network.connections)
            {
                LM.Log("Ping " + temp.ipAddress + " is " + Network.GetAveragePing(temp) + " ms");
            }
        }
    }
}
