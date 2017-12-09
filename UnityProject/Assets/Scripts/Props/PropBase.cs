using UnityEngine;
using System.Collections;

public enum PropType
{
    BurningBottle,
    GrassCutter,
    GrassGrowthing,
    Detecter,
    MineDetecter
}

public class PropBase : MonoBehaviour {

    public PropType type;
    public UISprite sprite;
    public UISprite icon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
