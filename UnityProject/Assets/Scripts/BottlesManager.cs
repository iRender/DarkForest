using UnityEngine;
using System.Collections;

public class BottlesManager : MonoBehaviour {

	public GameObject m_prefabBottle;

	public BurningBottle CreateBottle()
	{
		GameObject goBullet = GameObject.Instantiate (m_prefabBottle);
		goBullet.transform.parent = transform;
		goBullet.transform.localScale = Vector3.one;
		goBullet.transform.localPosition = Vector3.zero;
		return goBullet.GetComponent<BurningBottle> ();
	}

	public void DestroyBottle(BurningBottle bb)
	{
		GameObject.Destroy (bb.gameObject);
	}
}
