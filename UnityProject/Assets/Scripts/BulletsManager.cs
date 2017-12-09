using UnityEngine;
using System.Collections;

public class BulletsManager : MonoSingleton<BulletsManager> {
	public GameObject m_prefabBullet;
	public float m_flySpeed;

	public MyBullet CreateBullet()
	{
		GameObject goBullet = GameObject.Instantiate (m_prefabBullet);
		goBullet.transform.parent = transform;
		return goBullet.GetComponent<MyBullet> ();
	}
}
