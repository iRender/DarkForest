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

	public PlayerData data;

	public UISpriteAnimation m_spAnim;
	public UISprite m_sp;

	public float m_initMoveSpeed;
	public float m_initRunSpeed;
	public Vector2 m_moveSpeed = Vector2.zero;

	public Vector2 MoveSpeed {
		set { 
			if (value == Vector2.zero) {
				Idle ();
			}
			m_moveSpeed = value;
		}
	}

	public float MoveSpeedValue {
		get { 
			return m_moveSpeed.magnitude;
		}
		set { 
			m_moveSpeed = MoveDir * value;
		}
	}

	public Vector2 MoveDir {
		get {
			return m_moveSpeed.normalized;
		}
		set {
			m_moveSpeed = value.normalized * MoveSpeedValue;
		}
	}

	public Vector2 Current2DPos {
		get { 
			Vector2 vec2 = Vector2.zero;
			vec2.x = transform.localPosition.x;
			vec2.y = transform.localPosition.y;
			return vec2;
		}
	}

	public float PosYValue {
		get { 
			return transform.localPosition.y;
		}
	}

	public string m_idleAnimPre;
	public string m_walkAnimPre;
	public string m_runAnimPre;
	public string m_deadAnimPre;

	public ViewPort m_vp;
	public float heigtOfGun;
	public bool m_bDead;

	void Awake ()
	{
		Idle ();

		OnAwake ();
	}

	public virtual void OnAwake ()
	{
		
	}

	void Start ()
	{
		OnStart ();

		if (data != null) {
			m_id = data.guid;
		}
		RolesManager.ins.AddRole (this);
	}

	public virtual void OnStart ()
	{
		
	}

	public virtual void MoveToward (Vector2 direction)
	{
		m_moveSpeed = direction.normalized * m_initMoveSpeed; 
		PlayRunAnim ();
	}

	public virtual void Move (Vector2 delta_pos)
	{
		Vector2 targetPos = Current2DPos + delta_pos;
		TweenPosition.Begin (gameObject, 0, targetPos);
		if (delta_pos.x > 0)
			FaceRight ();
		else
			FaceLeft ();
		PlayRunAnim ();
	}

	public virtual void MoveTo (Vector2 target_pos)
	{
		float dis = Vector2.Distance (Current2DPos, target_pos);
		float duration = dis / MoveSpeedValue;
		TweenPosition tp = TweenPosition.Begin (gameObject, duration, target_pos);
		tp.method = UITweener.Method.Linear;
	}

	public void Idle ()
	{
		m_moveSpeed = Vector2.zero;
		PlayIdleAnim ();
	}

	public void Attack (Vector2 dir)
	{
		MyBullet bullet = GameManager.ins.m_bulletsManager.CreateBullet ();
		bullet.m_owner = this;
		Vector2 rolePos = Current2DPos;
		rolePos.y += heigtOfGun;
		bullet.transform.localPosition = rolePos;
		bullet.m_moveSpeed = dir.normalized * GameManager.ins.m_bulletsManager.m_flySpeed;
		bullet.RotateTo (dir);
	}

	public void Dead()
	{
		PlayDeadAnim ();
		m_moveSpeed = Vector2.zero;
		m_bDead = true;
	}

	void Update ()
	{
		if (MoveSpeedValue > 0) {
			Vector2 deltaPos = m_moveSpeed * Time.deltaTime;
			Move (deltaPos);
		}

		OnUpdate ();
	}

	public virtual void OnUpdate ()
	{
		
	}

	public void PlayRunAnim ()
	{
		m_spAnim.namePrefix = m_runAnimPre;
	}

	public void PlayIdleAnim ()
	{
		m_spAnim.namePrefix = m_idleAnimPre;
	}

	public void PlayWalkAnim ()
	{
		m_spAnim.namePrefix = m_walkAnimPre;
	}

	public void PlayDeadAnim ()
	{
		m_spAnim.loop = false;
		m_spAnim.namePrefix = m_deadAnimPre;
	}

	public void SetDepth (int depth)
	{
		m_sp.depth = depth;
	}

	public void FaceLeft ()
	{
		m_sp.flip = UIBasicSprite.Flip.Nothing;
	}

	public void FaceRight ()
	{
		m_sp.flip = UIBasicSprite.Flip.Horizontally;
	}

	public void Collide_Collision (Collision2D coll)
	{
		GameManager.Log (coll.gameObject.name);
	}

	public void Collide_Collider (Collider2D coll)
	{
		GameManager.Log (coll.gameObject.name);
	}

	public virtual void OnStateChanage (int state)
	{
		Debug.Log ("Role:" + state);
	}
}
