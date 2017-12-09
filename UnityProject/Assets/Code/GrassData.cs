using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GrassData : NetworkBehaviour
{
	[SyncVar (hook = "OnStateChanage")]
	public int mstate = 1;

	[ClientCallback]
	public void OnStateChanage (int state)
	{
		mstate = state;
		Debug.Log ("GrassData OnStateChanage:" + state);
	}


	[Command]
	public void Cmd_DoChangeState (int state)
	{
		mstate = state;
	}

	void Start ()
	{
		Cmd_AddGrass ();	
	}

	[Command]
	public void Cmd_AddGrass ()
	{
		if (isServer) {
			NetworkServer.Spawn (this.gameObject);
		}
	}

	void Update ()
	{
		if (isServer) {
			if (Input.GetKeyDown (KeyCode.N)) {
				Cmd_DoChangeState (11);
			}
		}
	}
}
