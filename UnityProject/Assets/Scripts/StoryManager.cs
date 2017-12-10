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
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			Next ();
		}
	}

	void Next()
	{
		if (index < stories.Length) {
			text.text = stories [index];
			anim.Play ("fadeinout");
			index++;
		} else {
			SceneManager.LoadScene("Main_Real1");
		}
	}

}
