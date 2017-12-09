using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerData : NetworkBehaviour
{
	public static PlayerData Create (GameObject player)
	{
		PlayerData pd = player.GetComponent<PlayerData> ();
		if (pd == null) {
			pd = player.AddComponent <PlayerData> ();	
		}
		return pd;
	}

	[SyncVar]
	public int hp = 100;
	[SyncVar]
	public int skill = 1;
	[SyncVar]
	public Vector3 synsPos;

	void FixedUpdate ()
	{
		TransmitPosition ();
		LerpPosition ();
	}

	void LerpPosition ()
	{
		if (!isLocalPlayer) {
			transform.position = Vector3.Lerp (transform.position, synsPos, Time.deltaTime);
		}
	}

	[Command]
	void CmdProvidePositionToServer (Vector3 pos)
	{
		synsPos = pos;
	}

	[ClientCallback]
	void TransmitPosition ()
	{
		if (isLocalPlayer) {
			CmdProvidePositionToServer (transform.position);
		}
	}
}
