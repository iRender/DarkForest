using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameNetDiscover : NetworkDiscovery
{
	public int MatchTime = 5;

	private bool matched = false;

	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
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

		StartAsClient ();

		LeanTween.delayedCall (MatchTime, () => {

			if (matched == false) {
				NetworkManager.singleton.StartHost ();
				StartAsServer ();
			}

		});
	}
}
