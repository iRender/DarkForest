using UnityEngine;
using System.Collections;

public class MyBullet : MonoBehaviour
{
	public Vector2 m_moveSpeed = Vector2.zero;
	public Role m_owner;

	public Vector2 Current2DPos
	{
		get
		{ 
			Vector2 vec2 = Vector2.zero;
			vec2.x = transform.localPosition.x;
			vec2.y = transform.localPosition.y;
			return vec2;
		}
	}

	public void Move(Vector2 delta_pos)
	{
		Vector2 targetPos = Current2DPos + delta_pos;
		TweenPosition.Begin (gameObject, 0, targetPos);
	}

	public void MoveTo(Vector2 target_pos, float duration)
	{
		TweenPosition.Begin (gameObject, duration, target_pos);
	}

	void Update()
	{
		if (m_moveSpeed != Vector2.zero) {
			Vector2 deltaPos = m_moveSpeed * Time.deltaTime;
			Move (deltaPos);
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		string collGOName = coll.gameObject.name;
		if (string.Equals(collGOName, "xxx"))
		{
			Role role = coll.transform.parent.GetComponent<Role> ();
			if (role != null && role.m_id != m_owner.m_id) {
				m_moveSpeed = Vector2.zero;
				Vector2 curPos = transform.localPosition;
				curPos.y -= role.heigtOfGun;
				MoveTo (curPos, 0.5f);
			}
		}
		if (string.Equals(collGOName, "Anim"))
		{
			Role role = coll.transform.parent.GetComponent<Role> ();
			if (role != null && role.m_id != m_owner.m_id) {
				role.Dead ();
				GameManager.ins.m_bulletsManager.DestroyBullet (this);
			}
		}
	}

	public void RotateTo(Vector2 targetDir)
	{
		float angle = Vector2.Angle (Vector2.right, targetDir);
		if (targetDir.y < 0) {
			angle = 360 - angle;
		}
		Quaternion quat = transform.localRotation;
		Vector3 localRot = quat.eulerAngles;
		localRot.z = angle + 180;
		quat.eulerAngles = localRot;
		transform.localRotation = quat;
		//		Rotate (-angle);
	}
}
