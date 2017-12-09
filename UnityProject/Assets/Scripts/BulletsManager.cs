using UnityEngine;
using System.Collections;

public class BulletsManager : MonoBehaviour {
	public GameObject m_prefabBullet;
	public float m_flySpeed;

	public MyBullet CreateBullet()
	{
		GameObject goBullet = GameObject.Instantiate (m_prefabBullet);
		goBullet.transform.parent = transform;
		goBullet.transform.localScale = Vector3.one;
		return goBullet.GetComponent<MyBullet> ();
	}
}
