using UnityEngine;
using System.Collections;

public class BurningBottle : PropBase {

	public float distance = 400;
	public float speed;
	public UISpriteAnimation anim;
	public string idle;
	public string fly;
	public GrassLand land;
	public Transform bottlesManager;

	private Vector3 des;

	public void Throw(Vector3 to, float duration)
	{
		transform.parent = bottlesManager;
		transform.localScale = Vector3.one;
		anim.namePrefix = fly;
		des = transform.localPosition + to.normalized * distance;
		TweenPosition.Begin (this.gameObject, duration, des);
		//TweenRotation.Begin (this.gameObject, duration, Quaternion.Euler (Vector3.forward * 1000));
		Invoke ("Expolosion", duration);
	}

	void Expolosion()
	{
		Vector3 v = des - land.transform.localPosition;
		land.BurningGrass (new Vector2 (v.x, v.y));
		Destroy (this.gameObject);
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			Throw (Vector3.one, 1);
		}
	}

	public void OnCollisionEnter(Collision col)
	{
		Expolosion ();

	}

}
