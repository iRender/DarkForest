using UnityEngine;
using System.Collections;

public class BulletsManager : MonoSingleton<BulletsManager> {
	public GameObject m_prefabBullet;

	public Bullet CreateBullet()
	{
		GameObject goBullet = GameObject.Instantiate (m_prefabBullet);
		goBullet.transform.parent = transform;
		return goBullet.GetComponent<Bullet> ();
	}
}
