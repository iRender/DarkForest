using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameTile : MonoBehaviour 
{
	public static int ids;
	public int m_id;
	void Awake()
	{
		m_id = ++ids;
	}
	[HideInInspector]
	public int row;
	[HideInInspector]
	public int column;
	public int size;
	public UISprite sprite;
}
