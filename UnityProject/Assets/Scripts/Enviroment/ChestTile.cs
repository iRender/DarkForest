using UnityEngine;
using System.Collections;

public class ChestTile : ItemTile {

	public UISpriteAnimation anim;
	public PropType proptype;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger:" + collision.gameObject.name);
    }

	public void Open() {
		Debug.Log ("Open");
		anim.namePrefix = "get_tresure";
		Destroy (gameObject, 1f);
    }

}
