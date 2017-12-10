using UnityEngine;
using System.Collections;

public class TestButton : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		for (int i = 1; i < 5; i++) {
			CameraController c = GameObject.Find ("Camera" + i).GetComponent<CameraController> ();
			Role r = GameObject.Find ("P" + i).GetComponent<Role> ();
			CullManager.Me.Register (c, r);
		}


		GameObject g1 = GameObject.Find ("B1");
		UIEventListener.Get (g1).onClick = g => {
			Messenger.Broadcast<int, int> (GameEventType.Role_Enter_OtherVision, 0, 1);
			Messenger.Broadcast<int> (GameEventType.Role_InGrass, 0);
		};

		GameObject g2 = GameObject.Find ("B2");
		UIEventListener.Get (g2).onClick = g => {
			Messenger.Broadcast<int> (GameEventType.Role_InGrass, 1);
		};

		GameObject g3 = GameObject.Find ("B3");
		UIEventListener.Get (g3).onClick = g => {
			Messenger.Broadcast<int> (GameEventType.Role_InGrass, 2);
		};

		GameObject g4 = GameObject.Find ("B4");
		UIEventListener.Get (g4).onClick = g => {
			Messenger.Broadcast<int> (GameEventType.Role_InGrass, 3);
		};
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
