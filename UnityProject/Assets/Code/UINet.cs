using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class UINet : MonoBehaviour
{
	public GameNetDiscover net_gm;

	void OnClick ()
	{
		net_gm.Run ();
	}
}
