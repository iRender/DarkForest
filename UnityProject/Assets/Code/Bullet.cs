using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int damage = 50;

	void OnCollisionEnter(Collision collision)
	{
		var hit = collision.gameObject;
		var health = hit.GetComponent<Health>();
		if (health  != null)
		{
			health.TakeDamage(damage);
		}

		Destroy(gameObject);
	}
}
