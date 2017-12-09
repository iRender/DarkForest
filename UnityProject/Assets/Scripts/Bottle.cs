using UnityEngine;
using System.Collections;

public class Bottle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MoveTo(Vector2 target_pos, float duration, System.Action callback)
	{
		TweenPosition tp = TweenPosition.Begin (gameObject, duration, target_pos);
		tp.SetOnFinished (() => {
			callback();
		});
	}
}
