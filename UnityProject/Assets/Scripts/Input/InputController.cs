using UnityEngine;
using System.Collections;
using Rewired;

public class InputController : MonoBehaviour {

	[SerializeField] private int m_playerId;

	private Player m_player;

	void Start () {
		m_player = ReInput.players.GetPlayer (m_playerId);
	}
		
	void Update()
	{
		Vector2 moveDir = Vector2.zero;
//		float xValueFromK = Input.GetAxis ("Horizontal");
//		float yValueFromK = Input.GetAxis ("Vertical");
		if (m_player.GetAxis("MoveX")<0) {
			moveDir.x += -1;
		}
		if (m_player.GetAxis("MoveX")>0) {
			moveDir.x += 1;
		}
		if (m_player.GetAxis("MoveY")>0) {
			moveDir.y += 1;
		}
		if (m_player.GetAxis("MoveY")<0) {
			moveDir.y += -1;
			MyselfPlayer.m_instance.m_vp.Rotate (90);
		}
//		if (xValueFromK == 1 || yValueFromK == 1) {
//			moveDir.x = xValueFromK;
//			moveDir.y = yValueFromK;
//		}
		MyselfPlayer.m_instance.MoveSpeed = moveDir * MyselfPlayer.m_instance.m_initMoveSpeed;

			
//		float xValueFromM = Input.GetAxis ("Mouse X");
//		float yValueFromM = Input.GetAxis ("Mouse Y");
//		Vector2 mouseMove = new Vector2 (xValueFromM, yValueFromM);
//		Vector2 mousePos = Input.mousePosition;
//		Vector2 screenCenter = new Vector2 (Screen.width / 2, Screen.height / 2);
		Vector2 mouseDir = m_player.GetAxis2D("MoveX","MoveY");
		MyselfPlayer.m_instance.m_vp.RotateTo (mouseDir);
		MyselfPlayer.m_instance.m_gun.RotateTo (mouseDir);

		if (m_player.GetButtonDown("Fire1")) {
			MyselfPlayer.m_instance.Attack (mouseDir);
		}
		if (m_player.GetButtonDown("Fire2")) {

		}

		if (m_player.GetButtonDown("Fire3")) {
			MyselfPlayer.m_instance.Acce ();
		}
		if (m_player.GetButtonDown("Fire4")) {
			MyselfPlayer.m_instance.RevertAcce ();
		}
	}
}
