using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	void Start () {
	
	}

	void Update()
	{
		Vector2 moveDir = Vector2.zero;
//		float xValueFromK = Input.GetAxis ("Horizontal");
//		float yValueFromK = Input.GetAxis ("Vertical");
		if (Input.GetKey ("a")) {
			moveDir.x += -1;
		}
		if (Input.GetKey ("d")) {
			moveDir.x += 1;
		}
		if (Input.GetKey ("w")) {
			moveDir.y += 1;
		}
		if (Input.GetKey ("s")) {
			moveDir.y += -1;
		}
//		if (xValueFromK == 1 || yValueFromK == 1) {
//			moveDir.x = xValueFromK;
//			moveDir.y = yValueFromK;
//		}
		MyselfPlayer.m_instance.MoveDir = moveDir;
		if (Input.GetMouseButtonDown (0)) {
			
		}
		if (Input.GetMouseButtonDown(1)) {
			
		}
		//Debug.Log (Input.GetAxis ("Mouse X"));
		//Debug.Log (Input.GetAxis ("Mouse Y"));
	}
}
