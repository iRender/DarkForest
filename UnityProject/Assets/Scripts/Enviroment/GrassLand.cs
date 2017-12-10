using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LandType
{
	Soil,
	Grass,
	Stone,
	Tree,
	Pile
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
	public float weight = 1;
	[HideInInspector]
	public float max;
	[HideInInspector]
	public float min;
}


public class GrassLand : MonoBehaviour
{
	public float width;
	public float height;

	public int columns = 16;
	public int rows = 16;
	public UISprite soilPrefab;

	public int ChestCount = 5;
	public int BulletCount = 5;

	public ChestTile chestPrefab;
	public BulletTile bulletPrefab;

	public GameLand[] lands;

	private GameObject _root;
	private int tileSize = 165;

	public class Tile
	{
		public LandTile land;
		public ItemTile item;
		public int row;
		public int column;

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
		width = columns * tileSize;
		height = rows * tileSize;
	}

	void GenerateTile ()
	{
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
		tiles = new Tile[rows][];
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
				t.column = c;
				t.row = r;
			}
		}

		// Random Land
		float weights = 0;
		foreach (var land in lands) {
			weights += land.weight;
		}
		Debug.Log ("Weights: " + weights);
		foreach (var land in lands) {
			land.weight = land.weight / weights;
		}
		float w = 0;
		foreach (var land in lands) {
			land.min = w;
			land.max = w + land.weight;
			w = land.max;
		}


		for (int row = 0; row < rows; row++) {
			for (int column = 0; column < columns; column++) {
				float r = Random.Range (0.0f, 1.0f);
				Debug.Log ("Random: " + r);
				foreach (var land in lands) {
					if (land.tile.type != LandType.Soil && r >= land.min && r <= land.max) {
						Tile t = tiles [row] [column];
						t.land = Instantiate (land.tile);
						t.land.transform.parent = landLayer.transform;
						t.land.transform.localScale = Vector3.one;
						t.land.row = row;
						t.land.column = column;
						SetSprite (t.land.sprite, row, column, 300);
						Debug.Log ("type: " + land.tile.type + "min: " + land.min + "max: " + land.max);
						break;
					} 
				}
			}
		}

		// Add Chest
		AddChest(4, itemLayer);

	}

	void AddChest(int count, GameObject itemLayer)
	{
		int co = 0;
		List<Tile> emptyTiles = new List<Tile> ();
		for (int r = 0; r < rows; r++) {
			for (int c = 0; c < columns; c++) {
				Tile t = tiles [r] [c];
				if (t.landType == LandType.Soil && t.itemType == ItemType.None) {
					emptyTiles.Add (t);
					co++;
				}
			}
		}
		co = Mathf.Min (co, count);
		for (int i = 0; i < count; i++) {
			foreach (Tile empty in emptyTiles) {
				if (empty.itemType == ItemType.None) {
					empty.item = Instantiate (chestPrefab);
					empty.item.transform.parent = itemLayer.transform;
					empty.item.transform.localScale = Vector3.one;
					empty.item.row = empty.row;
					empty.item.column = empty.column;
					SetSprite (empty.item.sprite, empty.row, empty.column, 200);
					break;
				}
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
			uisprite.transform.localPosition = new Vector3 (l.x - (collider.offset.x - 82.5f), l.y - (collider.offset.y - 82.5f), l.z);
        }

	}

	void OnDrawGizmos ()
	{
	}

	public Tile GetTile(Vector2 pos) {
		int r = (int) (pos.y / tileSize);
		Debug.Log ("R" + r);
		if (r < 0 || r >= rows) {
			return null;
		}
		int c = (int) (pos.x / tileSize);
		if (c < 0 || c >= columns) {
			return null;
		}
		return tiles [r] [c];
	}

	public void BurningGrass(Vector2 pos)
	{
		Tile t = GetTile (pos);
//		Debug.Log ("Tile: " + t.column);
		if (t != null) {
			for (int r = t.row-2; r <= t.row+2; r++) {
				for (int c = t.column-2; c < t.column+2; c++) {
					if (r >= 0 && r < rows && c >= 0 && c < columns) {
						Tile tile = tiles [r] [c];
						if (tile.landType == LandType.Grass) {
							GrassTile grass = tile.land as GrassTile;
							grass.Burn ();
							Debug.Log ("Burn");
						}
					}
				}
			}
		}
	}

}
