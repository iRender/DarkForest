using UnityEngine;
using System.Collections;

public class MyselfPlayer : Role
{
	public static MyselfPlayer m_instance;
	public StateMachine<State> m_sm;

	public override void OnAwake ()
	{
		m_instance = this;
		m_type = E_Type.Myself;
	}

	public override void OnStart ()
	{
		MoveCamera ();
		Debug.LogWarning ("MyselfPlayer:" + (data != null ? data.guid.ToString() : ""));
	}

	public override void OnUpdate ()
	{
		if (data != null) {
			if (data.isMyself && Input.GetKeyDown (KeyCode.M)) {
				data.Cmd_DoChangeState (10);
			}
		}
	}

	public override void Move (Vector2 delta_pos)
	{
		base.Move (delta_pos);

		MoveCamera ();
	}

	public void MoveCamera ()
	{
//		Vector3 cameraPos = GameManager.ins.m_cameraController.transform.localPosition;
//		cameraPos.x = transform.localPosition.x;
//		cameraPos.y = transform.localPosition.y;
//		GameManager.ins.m_cameraController.transform.localPosition = cameraPos;
	}

	public override void OnStateChanage (int state)
	{
		base.OnStateChanage (state);
		Debug.Log ("MyselfPlayer:" + state);
	}

	public void Collide_Collider (Collider2D coll)
	{
		GameManager.Log (coll.gameObject.name);

	}
}
