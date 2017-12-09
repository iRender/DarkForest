using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewPort : MonoBehaviour {
	public Collider2D m_viewPortColli;
	public float m_rotateSpeed;

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

	public void Rotate(float euler_angle)
	{
		transform.Rotate (0, 0, euler_angle);
		float duration = euler_angle / m_rotateSpeed;
		Quaternion qua = new Quaternion ();
		Vector3 eulerAngle = qua.eulerAngles;
		eulerAngle.z = euler_angle;
		qua.eulerAngles = eulerAngle;
		TweenRotation.Begin (gameObject, 0, qua);
	}

	public void RotateTo(Vector2 targetDir)
	{
		float angle = Vector2.Angle (Vector2.right, targetDir);
		if (targetDir.y < 0) {
			angle = 360 - angle;
		}
		Quaternion quat = transform.localRotation;
		Vector3 localRot = quat.eulerAngles;
		localRot.z = angle - 180;
		quat.eulerAngles = localRot;
		transform.localRotation = quat;
//		Rotate (-angle);
	}

	public List<GrassTile> m_listGrass = new List<GrassTile>();

	public void ViewHide()
	{
		foreach (GrassTile gt in m_listGrass) {
			gt.ViewHide ();
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		string goName = coll.gameObject.name;
		if (ObjectNamesManager.GetType(goName) == ObjectNamesManager.ObjectType.Grass) {
			GrassTile gt = coll.GetComponent<GrassTile> ();
			gt.ViewClear ();
			m_listGrass.Add (gt);
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		string goName = coll.gameObject.name;
		if (ObjectNamesManager.GetType(goName) == ObjectNamesManager.ObjectType.Grass) {
			GrassTile gt = coll.GetComponent<GrassTile> ();
			gt.ViewHide ();
			m_listGrass.Remove (gt);
		}
	}
}
