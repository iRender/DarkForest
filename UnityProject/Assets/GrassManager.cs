﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GrassManager : NetworkBehaviour
{
	public GameObject grassPrefab;

	private GameObject _grass;

	public void AddGrass ()
	{
		Cmd_AddGrass ();
	}

	[Command]
	void Cmd_AddGrass ()
	{
		_grass = (GameObject)Instantiate (
			grassPrefab,
			Vector3.zero,
			Quaternion.identity);

		NetworkServer.Spawn (_grass);
	}

	void Update ()
	{
		if (isServer) {
			if (Input.GetKeyDown (KeyCode.N)) {
				GrassData gd = _grass.GetComponent<GrassData> ();
				gd.Cmd_DoChangeState (gd.mstate + 1);
			}
		}
	}
}
