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
		Initialize ();
		StartAsServer ();

		NetworkManager.singleton.networkPort = 4444;
		NetworkManager.singleton.StartHost ();
	}
}