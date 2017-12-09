using UnityEngine;
using System.Collections;

public class MyBullet : MonoBehaviour
{
	public Vector2 m_moveSpeed = Vector2.zero;

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
		GameManager.Log ("111");
		Role role = coll.GetComponent<Role> ();
		Debug.Log (role);
		if (role != null) {
			m_moveSpeed = Vector2.zero;
			Vector2 curPos = transform.localPosition;
			curPos.y -= role.heigtOfGun;
			MoveTo (curPos, 0.5f);
		}
	}
}
