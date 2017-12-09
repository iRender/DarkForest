using UnityEngine;
using System.Collections;

public class AI_Simple : MonoBehaviour {

	public GrassLand m_grassL;
	public Vector2 GrassLPos
	{
		get
		{
			return new Vector2(m_grassL.transform.localPosition.x, m_grassL.transform.localPosition.y);
		}
	}
	public Role role;
	// Use this for initialization
	void Start () {
		role = GetComponent<Role> ();
		role.m_initMoveSpeed = 200;
		StartCoroutine (IE_DO());
	}

	IEnumerator IE_DO()
	{
		bool b = false;
		Vector2 lbPos = m_grassL.transform.localPosition;
		role.MoveTo (lbPos, () => {
			b = true;
		});
		while (!b) {
			yield return 0;
		}

		b = false;
		Vector2 ltPos = new Vector2(0, m_grassL.height) + GrassLPos;
		role.MoveTo(ltPos, () => {
			b = true;
		});
		while (!b) {
			yield return 0;
		}

		b = false;
		Vector2 rtPos = new Vector2(m_grassL.width, m_grassL.height) + GrassLPos;
		role.MoveTo(rtPos, () => {
			b = true;
		});
		while (!b) {
			yield return 0;
		}

		Vector2 rbPos = new Vector2(m_grassL.width, 0) + GrassLPos;
		role.MoveTo(rbPos, () => {
			Start();
		});
	}

	public void Do(System.Action callback)
	{
		
		Vector2 lbPos = m_grassL.transform.localPosition;
		role.MoveTo (lbPos, () => {
			Vector2 ltPos = new Vector2(0, m_grassL.height) + GrassLPos;
			role.MoveTo(ltPos, () => {
				Vector2 rtPos = new Vector2(m_grassL.width, m_grassL.height) + GrassLPos;
				role.MoveTo(rtPos, () => {
					Vector2 rbPos = new Vector2(0, m_grassL.height) + GrassLPos;
					role.MoveTo(ltPos, () => {

					});
				});
			});
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
