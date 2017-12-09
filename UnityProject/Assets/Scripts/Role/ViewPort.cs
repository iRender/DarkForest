using UnityEngine;
using System.Collections;

public class ViewPort : MonoBehaviour {
	public Collider2D m_viewPortColli;

	void Awake()
	{
		m_viewPortColli = GetComponent<BoxCollider2D> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Debug.Log (coll.gameObject.name);
	}
}
