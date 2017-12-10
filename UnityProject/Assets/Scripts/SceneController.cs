using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	public string sceneName = "";
	public int sceneId = 0;
	
	void GotoScene () {
		if (sceneName == "") {
			SceneManager.LoadScene (sceneId);
		} 
		else {
			SceneManager.LoadScene (sceneName);
		}
	}
}
