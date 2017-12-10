using UnityEngine;
using System.Collections;

public class GameManager : MonoSingleton<GameManager> {

	public CameraController m_cameraController;
	public BulletsManager m_bulletsManager;
	public BottlesManager m_bottleManager;
	public GrassLand m_grassLand;

	public bool bOver;
	public float time;
	public float customTime;
	public static void Log(string str)
	{
		Debug.Log ("haha:" + str);
	}

	void Update()
	{
		if (bOver) {
			time += Time.deltaTime;
			if (time >= customTime) {
				Application.LoadLevel (4);
			}
		}
	}

	public void GoToGameOver()
	{
		bOver = true;
		time = 0;
	}
}
