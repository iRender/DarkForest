using UnityEngine;
using System.Collections;

public class MyselfPlayer : Role
{
	public PlayerData data;
	
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
//		Debug.LogWarning ("MyselfPlayer:" + data.guid);
	}

	public override void OnUpdate ()
	{
		
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
}
