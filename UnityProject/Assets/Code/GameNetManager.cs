using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameNetManager : NetworkManager
{
	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
	{
		var player = (GameObject)GameObject.Instantiate (playerPrefab, Vector3.zero, Quaternion.identity);  
		NetworkServer.AddPlayerForConnection (conn, player, playerControllerId);  
	}

	public override void OnServerRemovePlayer (NetworkConnection conn, UnityEngine.Networking.PlayerController player)
	{
		base.OnServerRemovePlayer (conn, player);
	}
}
