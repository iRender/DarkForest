using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public Transform m_myselfTrans;
	public Transform m_mainPanelTrans;

	void Start () {
		
	}

	void Update ()
	{
		Vector3 myselfLocalPos = m_myselfTrans.localPosition;
		Vector3 localPos = transform.localPosition;
		localPos.x = myselfLocalPos.x;
		localPos.y = myselfLocalPos.y;
		transform.localPosition = localPos;
	}
}
