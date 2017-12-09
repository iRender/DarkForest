using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetHUD : MonoBehaviour {

	private string IpAddress = "172.26.62.105";
	private string Port = "1990";

	// Use this for initialization
	void Start () {
		CreateHost ();
	}

	public void CreateHost()
	{
		NetworkManager.singleton.networkPort = int.Parse (Port);
		NetworkManager.singleton.StartServer ();
	}

	public void Connect()
	{
		NetworkManager.singleton.networkAddress = IpAddress;
		NetworkManager.singleton.networkPort = int.Parse (Port);
		NetworkManager.singleton.StartClient ();		
	}

	public void Disconnect()
	{
		NetworkManager.singleton.StopHost ();
	}
}
