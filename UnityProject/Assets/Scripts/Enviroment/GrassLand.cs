﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LandType
{
	Soil,
	Grass
}

public enum ItemType
{
	None,
	Chest,
	Bonfire
}

[System.Serializable]
public struct Int2
{
	public int row;
	public int column;
}

[System.Serializable]
public class GameLand
{
	public LandTile tile;
	public Int2[] areas;
}

[System.Serializable]
public class GameItem
{
	public ItemTile tile;
	public Int2[] areas;
}


public class GrassLand : MonoBehaviour
{

	public int columns = 3;
	public int rows = 3;
	public UISprite soilPrefab;

	public GameLand[] lands;
	public GameItem[] items;

	private GameObject _root;
	private int tileSize = 165;

	public class Tile
	{
		public LandTile land;
		public ItemTile item;

		public LandType landType {
			get {
				return land == null ? LandType.Soil : land.type;
			}
		}

		public ItemType itemType {
			get {
				return item == null ? ItemType.None : item.type;
			}
		}
	}

	private Tile[][] tiles;

	void Awake ()
	{
		_root = this.gameObject;
	}

	void Start ()
	{
		GenerateTile ();
	}

	void GenerateTile ()
	{

		tiles = new Tile[rows][];

		GameObject emptyLayer = new GameObject ("Empty");
		GameObject itemLayer = new GameObject ("Item");
		GameObject landLayer = new GameObject ("Land");
		emptyLayer.transform.parent = _root.transform;
		itemLayer.transform.parent = _root.transform;
		landLayer.transform.parent = _root.transform;
		emptyLayer.transform.localScale = Vector3.one;
		itemLayer.transform.localScale = Vector3.one;
		landLayer.transform.localScale = Vector3.one;
        emptyLayer.transform.localPosition = Vector3.zero;
        itemLayer.transform.localPosition = Vector3.zero;
        landLayer.transform.localPosition = Vector3.zero;


		// Empty Tile
		for (int r = 0; r < rows; r++) {
			tiles [r] = new Tile[columns];
			for (int c = 0; c < columns; c++) {
				UISprite soil = Instantiate<UISprite> (soilPrefab);
				soil.transform.parent = emptyLayer.transform;
				soil.transform.localScale = Vector3.one;
				soil.transform.localPosition = new Vector3 (c * 495, r * 495, 0);
			}
		}



		rows *= 3;
		columns *= 3;

		tiles = new Tile[rows][];

		for (int r = 0; r < rows; r++) {
			tiles [r] = new Tile[columns];
			for (int c = 0; c < columns; c++) {
				Tile t;
				t = new Tile ();
				tiles [r] [c] = t;
			}
		}

		// Item Tile
		foreach (GameItem item in items) {
			foreach (Int2 area in item.areas) {
				Tile t = tiles [area.row][area.column];
				t.item = Instantiate<ItemTile> (item.tile);
				t.item.transform.parent = itemLayer.transform;
				t.item.transform.localScale = Vector3.one;
				t.item.row = area.row;
				t.item.column = area.column;
				SetSprite (t.item.sprite, area.row, area.column, 200);
			}
		}

		// Land Tile
		foreach (GameLand land in lands) {
			foreach (Int2 area in land.areas) { 
				Tile t = tiles [area.row][area.column];
				t.land = Instantiate<LandTile> (land.tile);
				t.land.transform.parent = landLayer.transform;
				t.land.transform.localScale = Vector3.one;
				t.land.row = area.row;
				t.land.column = area.column;
				SetSprite (t.land.sprite, area.row, area.column, 300);
			}
		}
	}

	void SetSprite (UISprite uisprite, int row, int column, int depth)
	{
        uisprite.gameObject.layer = SortingLayer.NameToID("UI");
		uisprite.depth = depth - row;
		uisprite.transform.localPosition = new Vector3 (column * tileSize, row * tileSize, 0);

        BoxCollider2D collider = uisprite.gameObject.GetComponent<BoxCollider2D>();
        if (collider != null)
        {
			Vector3 l = uisprite.transform.localPosition;
			uisprite.transform.localPosition = new Vector3 (l.x, l.y + uisprite.height / 2 - collider.offset.y, l.z);
        }

	}

	void OnDrawGizmos ()
	{
	}

    public Tile GetTile(Vector2 point) {
        Tile t = new Tile();
        return t;
    }

}
