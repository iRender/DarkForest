using UnityEngine;
using System.Collections;
using Rewired;

public class InputController : MonoBehaviour {

	private int m_playerId
    {
        get { return m_myselfPlayer.m_id; }
    }
    private MyselfPlayer m_myselfPlayer;

	private Player m_player;
    Vector2 m_mouseDir = Vector2.down;


    void Start () {
        m_myselfPlayer = GetComponent<MyselfPlayer>();
        m_player = ReInput.players.GetPlayer(m_playerId);

    }
		
	void Update()
	{
		if (m_myselfPlayer.m_bDead)
			return;
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
            m_myselfPlayer.m_vp.Rotate (90);
		}
        //		if (xValueFromK == 1 || yValueFromK == 1) {
        //			moveDir.x = xValueFromK;
        //			moveDir.y = yValueFromK;
        //		}
        m_myselfPlayer.MoveSpeed = moveDir * m_myselfPlayer.m_initMoveSpeed;


        //		float xValueFromM = Input.GetAxis ("Mouse X");
        //		float yValueFromM = Input.GetAxis ("Mouse Y");
        //		Vector2 mouseMove = new Vector2 (xValueFromM, yValueFromM);
        //		Vector2 mousePos = Input.mousePosition;
        //		Vector2 screenCenter = new Vector2 (Screen.width / 2, Screen.height / 2);
        Vector2 mouseDir = m_player.GetAxis2D("MoveX","MoveY");
        if (mouseDir.sqrMagnitude != 0) m_mouseDir = mouseDir;

        m_myselfPlayer.m_vp.RotateTo (m_mouseDir);
        m_myselfPlayer.m_gun.RotateTo (m_mouseDir);

		if (m_player.GetButtonDown("Fire1")) {
            m_myselfPlayer.Attack (m_mouseDir);
		}
		if (m_player.GetButtonDown("Fire2")) {

		}

		if (m_player.GetButtonDown("Fire3")) {
            m_myselfPlayer.Acce ();
		}
		if (m_player.GetButtonDown("Fire4")) {
            m_myselfPlayer.RevertAcce ();
		}
	}
}
