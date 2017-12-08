using UnityEngine;
using System.Collections;

public class Role : MonoBehaviour
{
	public UISpriteAnimation m_animRun;
	public float m_speedValue;
	public bool m_isMoving;
	public Vector2 m_moveDir;
	public Vector2 MoveDir
	{
		get
		{
			return m_moveDir;
		}
		set
		{
			if (m_moveDir == Vector2.zero) {
				Idle ();
			}
			m_moveDir = value.normalized;
		}
	}

	void Awake()
	{
		m_animRun.Pause ();

		OnAwake ();
	}

	public virtual void OnAwake()
	{
		
	}
		
	public void MoveToward(Vector2 direction)
	{
		m_isMoving = true;
		MoveDir = direction;
		m_animRun.Play ();
	}

	public void Move(Vector2 delta_pos)
	{
		transform.Translate (delta_pos.x, delta_pos.y, 0);
		m_animRun.Play ();
	}

	public void Idle()
	{
		m_isMoving = false;
		m_moveDir = Vector2.zero;
		m_animRun.Pause ();
	}

	public void Attack()
	{
		
	}

	void Update()
	{
		if (m_isMoving) {
			float deltaDistance = Time.deltaTime * m_speedValue;
			Vector2 deltaPos = m_moveDir * deltaDistance;
			Move (deltaPos);
		}

		OnUpdate ();
	}

	public virtual void OnUpdate()
	{
		
	}
}
