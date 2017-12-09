using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameNetManager : NetworkManager
{
	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
	{
		base.OnServerAddPlayer (conn, playerControllerId);
	}

	public override void OnServerRemovePlayer (NetworkConnection conn, UnityEngine.Networking.PlayerController player)
	{
		base.OnServerRemovePlayer (conn, player);
	}
}
