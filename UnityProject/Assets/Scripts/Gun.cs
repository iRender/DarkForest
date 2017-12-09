using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public void RotateTo(Vector2 targetDir)
	{
		float angle = Vector2.Angle (Vector2.right, targetDir);
		if (targetDir.y < 0) {
			angle = 360 - angle;
		}
		Quaternion quat = transform.localRotation;
		Vector3 localRot = quat.eulerAngles;
		localRot.z = angle + 180;
		quat.eulerAngles = localRot;
		transform.localRotation = quat;
		//		Rotate (-angle);
	}
}
