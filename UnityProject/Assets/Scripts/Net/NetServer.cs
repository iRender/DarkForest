using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetServer : NetworkDiscovery
{

	int minPort = 10000;
	int maxPort = 10010;
	int defaultPort = 10000;

	// Use this for initialization
	void Start ()
	{
		Application.runInBackground = true;
		StartServer ();	
	}
	
	//Call to create a server
	public void StartServer ()
	{
		int serverPort = CreateServer ();
		if (serverPort != -1) {
			Debug.Log ("Server created on port : " + serverPort);
			broadcastData = serverPort.ToString ();
			Initialize ();
			StartAsServer ();

			NetworkManager.singleton.StartHost ();

		} else {
			Debug.Log ("Failed to create Server");
		}
	}

	//Creates a server then returns the port the server is created with. Returns -1 if server is not created
	private int CreateServer ()
	{
		int serverPort = -1;
		//Connect to default port
		bool serverCreated = NetworkServer.Listen (defaultPort);
		if (serverCreated) {
			serverPort = defaultPort;
			Debug.Log ("Server Created with deafault port");
		} else {
			Debug.Log ("Failed to create with the default port");
			//Try to create server with other port from min to max except the default port which we trid already
			for (int tempPort = minPort; tempPort <= maxPort; tempPort++) {
				//Skip the default port since we have already tried it
				if (tempPort != defaultPort) {
					//Exit loop if successfully create a server
					if (NetworkServer.Listen (tempPort)) {
						serverPort = tempPort;
						break;
					}

					//If this is the max port and server is not still created, show, failed to create server error
					if (tempPort == maxPort) {
						Debug.LogError ("Failed to create server");
					}
				}
			}
		}
		return serverPort;
	}
}