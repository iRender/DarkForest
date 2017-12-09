using UnityEngine;
using System.Collections;

public class GameManager : MonoSingleton<GameManager> {

	public CameraController m_cameraController;
	public BulletsManager m_bulletsManager;
	public static void Log(string str)
	{
		Debug.Log ("haha:" + str);
	}
}
