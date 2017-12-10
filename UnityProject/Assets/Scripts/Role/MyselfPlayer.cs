using UnityEngine;
using System.Collections;

public class MyselfPlayer : Role
{
	public CameraController cameraCon;
	public override void OnAwake ()
	{
		m_type = E_Type.Myself;
		CullManager.Me.Register (cameraCon, this);
		MissInGrass ();
	}

	public override void OnStart ()
	{
		MoveCamera ();
//		Debug.LogWarning ("MyselfPlayer:" + (data != null ? data.guid.ToString() : ""));
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
		
	public override void Collide_Collider_Enter (Collider2D coll)
	{
		string goName = coll.gameObject.name;
		if (ObjectNamesManager.GetType(goName) == ObjectNamesManager.ObjectType.Box) {
			ChestTile gt = coll.GetComponent<ChestTile> ();
		} 
		if (ObjectNamesManager.GetType(goName) == ObjectNamesManager.ObjectType.Grass) {
			m_countGrassColl++;
			OpenVP ();
			MissInGrass ();
		} 
	}

	public override void Collide_Collider_Exit(Collider2D coll)
	{
//		Debug.LogWarning("MyselfPlayer:Collide_Collider_Exit");
//		Debug.LogWarning (coll.gameObject.name);
		string goName = coll.gameObject.name;
		if (ObjectNamesManager.GetType(goName) == ObjectNamesManager.ObjectType.Grass) {
			m_countGrassColl--;
			if (m_countGrassColl <= 0) {
				CloseViewPort ();
				coll.GetComponent<GrassTile> ().ViewHide ();
				MissOutGrass ();
			}
		} 
		if (ObjectNamesManager.GetType(goName) == ObjectNamesManager.ObjectType.Role) {
			Role role = coll.transform.parent.GetComponent<Role> ();
			role.MissFromRole (m_id);
//			Debug.LogWarning ("leave from role");
		} 
		if (ObjectNamesManager.GetType(goName) == ObjectNamesManager.ObjectType.ViewPort) {
			Role role = coll.transform.parent.GetComponent<Role> ();
			role.MissFromRole (m_id);
//			Debug.LogWarning ("leave from vp");
		} 
	}


}
