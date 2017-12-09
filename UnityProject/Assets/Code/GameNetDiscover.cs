using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameNetDiscover : MonoBehaviour
{
	public NetworkDiscovery server;
	public NetworkDiscovery client;

	public int PlayerLimitNum = 4;
	public bool ShowGUI = true;

	bool running = false;

	public bool isNetConnect {
		get {
			return NetworkManager.singleton.isNetworkActive;
		}
	}

	void Start ()
	{
		running = false;

		server.showGUI = ShowGUI;
		client.showGUI = false;
	}

	public int MatchTime = 5;

	public bool matched = false;

	public void Run ()
	{
		if (running == true) {
			return;
		}

		running = true;

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


	void LateUpdate ()
	{
		if (running) {
			if (NetworkManager.singleton.numPlayers > PlayerLimitNum) {
				running = false;
				server.gameObject.SetActive (false);	
			}
		}
	}
}
