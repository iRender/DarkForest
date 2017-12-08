using UnityEngine;
using System.Collections;

public class MyselfPlayer : Role 
{
	public static MyselfPlayer m_instance;
	public StateMachine<State> m_sm;

	public override void OnAwake()
	{
		m_instance = this;
	}

	public override void OnUpdate()
	{
		if (m_moveDir != Vector2.zero) {
			float deltaDistance = Time.deltaTime * m_speedValue;
			Vector2 deltaPos = m_moveDir * deltaDistance;
			Move (deltaPos);
		}
	}
}
