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
			MyselfPlayer.m_instance.m_vp.Rotate (90);
		}
//		if (xValueFromK == 1 || yValueFromK == 1) {
//			moveDir.x = xValueFromK;
//			moveDir.y = yValueFromK;
//		}
		MyselfPlayer.m_instance.MoveSpeed = moveDir * MyselfPlayer.m_instance.m_initMoveSpeed;

		if (Input.GetMouseButtonDown (0)) {
			MyselfPlayer.m_instance.Attack ();
		}
		if (Input.GetMouseButtonDown(1)) {
			
		}
			
//		float xValueFromM = Input.GetAxis ("Mouse X");
//		float yValueFromM = Input.GetAxis ("Mouse Y");
//		Vector2 mouseMove = new Vector2 (xValueFromM, yValueFromM);
		Vector2 mousePos = Input.mousePosition;
		Vector2 screenCenter = new Vector2 (Screen.width / 2, Screen.height / 2);
		Vector2 mouseDir = screenCenter - mousePos;
		MyselfPlayer.m_instance.m_vp.RotateTo (mouseDir);
	}
}
