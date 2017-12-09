using UnityEngine;
using System.Collections;

public class BottlesManager : MonoBehaviour {

	public GameObject m_prefabBullet;

	public void CreateBottle()
	{
		GameObject goBullet = GameObject.Instantiate (m_prefabBullet);
		goBullet.transform.parent = transform;
		goBullet.transform.localScale = Vector3.one;
	}

	public void DestroyBullet()
	{
		
	}
}
