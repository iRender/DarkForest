using UnityEngine;
using System.Collections;

public class ColliderEvent : MonoBehaviour {
	public Role m_role;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		m_role.Collide_Collision (coll);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		m_role.Collide_Collider (coll);
	}
}
