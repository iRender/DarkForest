using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameNetDiscover : NetworkDiscovery
{
	public int MatchTime = 5;

	public bool matched = false;

	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
		Debug.LogWarning ("match");
		matched = true;

		NetworkManager.singleton.networkAddress = fromAddress;
		NetworkManager.singleton.StartClient ();
	}

	public void Run ()
	{
		Initialize ();

		if (matched == true) {
			return;
		}

		Debug.LogWarning ("StartAsClient");
		StartAsClient ();

		LeanTween.delayedCall (MatchTime, () => {
			if (matched == false) {
				Debug.LogWarning ("StartAsServer");
				NetworkManager.singleton.StartHost ();
				StartAsServer ();
			}
		});
	}
}
