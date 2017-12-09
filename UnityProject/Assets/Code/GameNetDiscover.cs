using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameNetDiscover : MonoBehaviour
{
	public NetworkDiscovery server;
	public NetworkDiscovery client;

	public bool ShowGUI = true;

	public bool isNetConnect {
		get {
			return NetworkManager.singleton.isNetworkActive;
		}
	}

	void Start ()
	{
		server.showGUI = ShowGUI;
		client.showGUI = false;
	}

	public int MatchTime = 5;

	public bool matched = false;

	public void Run ()
	{
		if (isNetConnect) {
			return;
		}

		client.gameObject.SetActive (true);

		LeanTween.delayedCall (MatchTime, () => {

			if (!isNetConnect) {
				client.gameObject.SetActive (false);
				server.gameObject.SetActive (true);
			}

		});
	}

	public void ShutDown ()
	{
		server.gameObject.SetActive (false);	
		client.gameObject.SetActive (false);	
	}

}
