using UnityEngine;
using System.Collections;

public class ChestTile : ItemTile {

    public UI2DSpriteAnimation open;
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
		Destroy (gameObject);
    }

}
