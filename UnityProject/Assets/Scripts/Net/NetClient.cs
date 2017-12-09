using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetClient : NetworkDiscovery
{

	// Use this for initialization
	void Start ()
	{
		StartClient ();
	}

	public void StartClient ()
	{
		Initialize ();
		StartAsClient ();
	}

	public override void OnReceivedBroadcast (string fromAddress, string fromPort)
	{
		Debug.LogWarning ("match");

		NetworkManager.singleton.networkAddress = fromAddress;
		NetworkManager.singleton.networkPort = 4444;
		NetworkManager.singleton.StartClient ();

		gameObject.SetActive (false);
	}
}
