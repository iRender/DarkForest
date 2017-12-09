using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TestNet : NetworkBehaviour
{
	[SyncVar]
	public int testA;

	// Use this for initialization
	void Start ()
	{
//		NetworkManager nm;
//		nm.StartServer ();
//		NetworkClient nc = nm.StartHost ();
//		NetworkClient sc = nm.StartClient ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
