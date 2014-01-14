using UnityEngine;
using System.Collections;

namespace UnityNetwork
{
    [RequireComponent(typeof(Rigidbody), typeof(NetworkView)) ,AddComponentMenu("UnityNetwork/NetworkRigidbody")]
    public class NetworkRigidbody : MonoBehaviour 
    {
        private double m_InterpolationBackTime = 0.15;
        private double m_ExtrapolationLimit = 0.3;
	
        internal struct State
        {
	        internal double timestamp;
	        internal Vector3 pos;
	        internal Vector3 velocity;
	        internal Quaternion rot;
	        internal Vector3 angularVelocity;
        }
	
        State[] m_BufferedState = new State[5];
        int m_TimestampCount;
	
	
	
        void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
        {
	        if (stream.isWriting)
	        {
		        Vector3 pos = transform.position;
		        Quaternion rot = transform.rotation;
		        Vector3 velocity = Vector3.zero;
		        Vector3 angularVelocity = Vector3.zero;

		        stream.Serialize(ref pos);
		        stream.Serialize(ref velocity);
		        stream.Serialize(ref rot);
		        stream.Serialize(ref angularVelocity);
	        }
	        else
	        {
		        Vector3 pos = Vector3.zero;
		        Vector3 velocity = Vector3.zero;
		        Quaternion rot = Quaternion.identity;
		        Vector3 angularVelocity = Vector3.zero;
		        stream.Serialize(ref pos);
		        stream.Serialize(ref velocity);
		        stream.Serialize(ref rot);
		        stream.Serialize(ref angularVelocity);
			
		        for (int i=m_BufferedState.Length-1;i>=1;i--)
		        {
			        m_BufferedState[i] = m_BufferedState[i-1];
		        }
			
		        State state;
		        state.timestamp = info.timestamp;
		        state.pos = pos;
		        state.velocity = velocity;
		        state.rot = rot;
		        state.angularVelocity = angularVelocity;
		        m_BufferedState[0] = state;
			
		        m_TimestampCount = Mathf.Min(m_TimestampCount + 1, m_BufferedState.Length);

		        for (int i=0;i<m_TimestampCount-1;i++)
		        {
			        if (m_BufferedState[i].timestamp < m_BufferedState[i+1].timestamp)
				        Debug.Log("State inconsistent");
		        }	
	        }
        }
	
        void Update () 
        {
	        if(!networkView.isMine)
	        {
	            double interpolationTime = Network.time - m_InterpolationBackTime;
		
	            if (m_BufferedState[0].timestamp > interpolationTime)
	            {
		            for (int i=0;i<m_TimestampCount;i++)
		            {
			            if (m_BufferedState[i].timestamp <= interpolationTime || i == m_TimestampCount-1)
			            {
				            State rhs = m_BufferedState[Mathf.Max(i-1, 0)];
				            State lhs = m_BufferedState[i];
					
				            double length = rhs.timestamp - lhs.timestamp;
				            float t = 0.0F;
				            if (length > 0.0001){
					            t = (float)((interpolationTime - lhs.timestamp) / length);
				            }
				            transform.position = Vector3.Lerp(lhs.pos, rhs.pos, t);
				            transform.rotation = Quaternion.Slerp(lhs.rot, rhs.rot, t);
				            return;
			            }
		            }
	            }
	            else
	            {
		            State latest = m_BufferedState[0];
			
		            float extrapolationLength = (float)(interpolationTime - latest.timestamp);
		            if (extrapolationLength < m_ExtrapolationLimit)
		            {
			            float axisLength = extrapolationLength * latest.angularVelocity.magnitude * Mathf.Rad2Deg;
			            Quaternion angularRotation = Quaternion.AngleAxis(axisLength, latest.angularVelocity);
				
			            transform.position = latest.pos + latest.velocity * extrapolationLength;
			            transform.rotation = angularRotation * latest.rot;
			            rigidbody.velocity = latest.velocity;
			            rigidbody.angularVelocity = latest.angularVelocity;
		            }
	            }
	        }
        }
        void Awake()
        {
	        if(!networkView.isMine)
	        {
	            State latest = m_BufferedState[0];
	            transform.position = latest.pos;
	            transform.rotation = latest.rot;
	            rigidbody.velocity = latest.velocity;
	            rigidbody.angularVelocity = latest.angularVelocity;
	        }
        }
    }

}
