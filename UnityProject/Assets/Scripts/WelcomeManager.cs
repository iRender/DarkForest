using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class WelcomeManager : MonoBehaviour {

	public AudioSource keyDown;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			keyDown.Play ();
		}
	}
}
