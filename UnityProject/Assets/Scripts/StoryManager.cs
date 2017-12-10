using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour {

	public string[] stories;
	public Text text;
	public Animator anim;

	int index = 0;

	// Use this for initialization
	void Start () {
		text.CrossFadeAlpha (1, 0f, false);
		text.CrossFadeAlpha (0, 2f, false);
		Invoke ("Next", 2f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			CancelInvoke ("Next");
			Next ();
		}
	}

	void Next()
	{
		if (index < stories.Length) {
			text.text = stories [index];
			text.CrossFadeAlpha (1, 0f, false);
			text.CrossFadeAlpha (0, 2f, false);
			Invoke ("Next", 2f);
			index++;
		} else {
			SceneManager.LoadScene("Main_Real1");
		}
	}

}
