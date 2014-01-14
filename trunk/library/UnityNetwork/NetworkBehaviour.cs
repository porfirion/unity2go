using UnityEngine;

namespace UnityNetwork
{
    [AddComponentMenu("UnityNetwork/NetworkBehaviour")]
    public abstract class NetworkBehaviour : MonoBehaviour
    {
        protected LogManager LM;

        protected virtual void Awake()
        {
            InitLogManager();

            LM.Log("Connection Status " + Network.TestConnection());

            if (Network.HavePublicAddress())
            {
                LM.Log("This machine has a public IP address");
            }
            else
            {
                LM.Log("This machine has a private IP address");
            }
        }

        protected virtual void InitLogManager()
        {
            LM = new LogManager();
        }
    }
}
