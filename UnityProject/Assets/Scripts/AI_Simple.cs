using UnityEngine;
using System.Collections;

public class AI_Simple : MonoBehaviour {

	public GrassLand m_grassL;
	// Use this for initialization
	void Start () {
		Role role = GetComponent<Role> ();
		role.m_initMoveSpeed = 200;
		Vector2 lbPos = m_grassL.transform.localPosition;
		role.MoveTo (lbPos, () => {
			
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
