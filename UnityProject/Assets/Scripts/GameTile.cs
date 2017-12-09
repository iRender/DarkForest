using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameTile : MonoBehaviour 
{
	[HideInInspector]
	public int row;
	[HideInInspector]
	public int column;
	public int size;
	public UISprite sprite;
}
