using UnityEngine;
using System.Collections;

public class Role : MonoBehaviour
{
	public enum E_Type
	{
		None,
		Myself
	}
	public E_Type m_type;

	public int m_id;

	public UISpriteAnimation m_animRun;
	public UISprite m_spRun;
	public UISpriteAnimation m_animIdle;
	public UISprite m_spIdle;
	public UISpriteAnimation m_animAttack;
	public UISprite m_spAttack;
	public UISpriteAnimation m_animDead;
	public UISprite m_spDead;

	public float m_initMoveSpeed;
	public Vector2 m_moveSpeed = Vector2.zero;
	public Vector2 MoveSpeed
	{
		set
		{ 
			if (value == Vector2.zero) {
				Idle ();
			}
			m_moveSpeed = value;
		}
	}
	public float MoveSpeedValue
	{
		get
		{ 
			return m_moveSpeed.magnitude;
		}
		set
		{ 
			m_moveSpeed = MoveDir * value;
		}
	}
	public Vector2 MoveDir
	{
		get
		{
			return m_moveSpeed.normalized;
		}
		set
		{
			m_moveSpeed = value.normalized * MoveSpeedValue;
		}
	}
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

	public float PosYValue
	{
		get
		{ 
			return transform.localPosition.y;
		}
	}

	void Awake()
	{
		m_animRun.Pause ();
		RolesManager.ins.AddRole (this);

		OnAwake ();
	}

	public virtual void OnAwake()
	{
		
	}
		
	public virtual void MoveToward(Vector2 direction)
	{
		MoveDir = direction;
		PlayRunAnim ();
	}

	public virtual void Move(Vector2 delta_pos)
	{
		transform.Translate (delta_pos.x, delta_pos.y, 0);
		PlayRunAnim ();
	}

	public virtual void MoveTo(Vector2 target_pos)
	{
		float dis = Vector2.Distance (Current2DPos, target_pos);
		float duration = dis / MoveSpeedValue;
		TweenPosition tp = TweenPosition.Begin (gameObject, duration, target_pos);
		tp.method = UITweener.Method.Linear;
	}

	public void Idle()
	{
		m_moveSpeed = Vector2.zero;
		PlayIdleAnim ();
	}

	public void Attack()
	{
		PlayAttackAnim ();
	}

	public void Gecao()
	{
		
	}

	void Update()
	{
		if (MoveSpeedValue > 0) {
			Vector2 deltaPos = m_moveSpeed * Time.deltaTime;
			Move (deltaPos);
		}

		OnUpdate ();
	}

	public virtual void OnUpdate()
	{
		
	}

	public void PlayRunAnim()
	{
		m_animRun.gameObject.SetActive (true);
		m_animIdle.gameObject.SetActive (false);
		m_animRun.Play ();
	}

	public void PlayIdleAnim()
	{
		m_animIdle.gameObject.SetActive (true);
		m_animRun.gameObject.SetActive (false);
		m_animIdle.Play ();
	}

	public void PlayGecaoAnim()
	{
		
	}

	public void PlayAttackAnim()
	{
		m_animIdle.gameObject.SetActive (false);
		m_animRun.gameObject.SetActive (false);
	}

	public void PlayDeadAnim()
	{
		m_animIdle.gameObject.SetActive (false);
		m_animRun.gameObject.SetActive (false);
	}

	public void SetDepth(int depth)
	{
		m_spIdle.depth = depth;
		m_spRun.depth = depth;
//		m_spAttack.depth = depth;
//		m_spDead.depth = depth;
	}
}
