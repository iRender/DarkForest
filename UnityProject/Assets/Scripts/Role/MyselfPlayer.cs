using UnityEngine;
using System.Collections;

public class MyselfPlayer : Role
{

	public override void OnAwake ()
	{
		m_type = E_Type.Myself;

		Camera camera = GameObject.Find ("Camera1").GetComponent<Camera> ();
		CullManager.Me.Active_CameraCullPlayer (camera, 1, true);
		CullManager.Me.Active_CameraCullPlayer (camera, 2, false);
		CullManager.Me.Active_CameraCullPlayer (camera, 3, true);
		CullManager.Me.Active_CameraCullPlayer (camera, 4, false);
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
		
	public override void Collide_Collider_Enter (Collider2D coll)
	{
		string goName = coll.gameObject.name;
		if (ObjectNamesManager.GetType(goName) == ObjectNamesManager.ObjectType.Box) {
			ChestTile gt = coll.GetComponent<ChestTile> ();
		} 
		if (ObjectNamesManager.GetType(goName) == ObjectNamesManager.ObjectType.Grass) {
			m_countGrassColl++;
			OpenVP ();
		} 
	}

	public override void Collide_Collider_Exit(Collider2D coll)
	{
		string goName = coll.gameObject.name;
		if (ObjectNamesManager.GetType(goName) == ObjectNamesManager.ObjectType.Grass) {
			m_countGrassColl--;
			if (m_countGrassColl <= 0) {
				CloseViewPort ();
				coll.GetComponent<GrassTile> ().ViewHide ();
			}
		} 
	}
}
