using UnityEngine;
using System.IO;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class NetworkScript : MonoBehaviour {
	string hostName = "localhost";
	int hostPort = 25001;
	
	private bool isConnected = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		if (GUI.Button (new Rect (0, 0, 200, 100), "Connect")) {
			connect();
		}
	}
	
	
	void connect() {
		try {
			TcpClient client = new TcpClient(hostName, hostPort);
			NetworkStream stream = client.GetStream();
			StreamWriter writer = new StreamWriter(stream);
			StreamReader reader = new StreamReader(stream);
			
			isConnected = true;
			
			Debug.Log("Connected!");
			
			writer.Write("Hello!");
			writer.Flush();
			
			writer.Close();
			reader.Close();
			
			
			
			stream.Close();
			client.Close();
			
			Debug.Log("Connection closed");
		}
		catch (SocketException ex) {
			Debug.LogException(ex);
		}
	}
}