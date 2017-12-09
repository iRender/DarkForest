using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LandType
{
	Soil,
	Grass1,
	Grass2
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
	public LandType type;
	public int size = 1;
	public Int2[] areas;
}

[System.Serializable]
public class GameItem
{
	public ItemType type;
	public int size = 1;
	public Int2[] areas;
}

[System.Serializable]
public class LandSprite
{
	public LandType type;
	public string sprite;
}

[System.Serializable]
public class ItemSprite
{
	public ItemType type;
	public string sprite;
}


public class GrassLand : MonoBehaviour
{

	public int columns = 10;
	public int rows = 10;
	public int tileSize = 60;

	public GameLand[] lands;
	public GameItem[] items;

	public UIAtlas atlas;
	public LandSprite[] landSprites;
	public ItemSprite[] itemSprites;

	public UISprite spritePrefab;

	private Dictionary<LandType, LandSprite> _landSprites = new Dictionary<LandType, LandSprite>();
	private Dictionary<ItemType, ItemSprite> _itemSprites = new Dictionary<ItemType, ItemSprite>();
	private UIRoot _root;


	private class Tile
	{
		public UISprite emptySprite;
		public UISprite landSprite;
		public UISprite itemSprite;
		public int row;
		public int column;
		public LandType landType;
		public ItemType itemType;
	}
	private Tile[][] tiles;

	void Awake()
	{
		foreach (var landSprite in landSprites)
		{
			_landSprites.Add(landSprite.type, landSprite);
		}

		foreach (var itemSprite in itemSprites)
		{
			_itemSprites.Add(itemSprite.type, itemSprite);
		}

		_root = UIRoot.list[0];
	}

	void Start()
	{
		GenerateTile();
	}

	void GenerateTile()
	{

		tiles = new Tile[rows][];

		GameObject emptyLayer = new GameObject("Empty");
		GameObject itemLayer = new GameObject("Item");
		GameObject landLayer = new GameObject("Land");
		emptyLayer.transform.parent = _root.transform;
		itemLayer.transform.parent = _root.transform;
		landLayer.transform.parent = _root.transform;
		emptyLayer.transform.localScale = Vector3.one;
		itemLayer.transform.localScale = Vector3.one;
		landLayer.transform.localScale = Vector3.one;

		// Empty Tile
		for (int r = 0; r < rows; r++)
		{
			tiles[r] = new Tile[columns];
			for (int c = 0; c < columns; c++)
			{
				UISprite uisprite = MakeSprite(emptyLayer.transform, _landSprites[LandType.Soil].sprite, r, c, 100);

				Tile t;
				t = new Tile();
				t.row = r;
				t.column = c;
				t.landType = LandType.Soil;
				t.itemType = ItemType.None;
				t.emptySprite = uisprite;
				tiles[r][c] = t;
			}
		}

		// Item Tile
		foreach (GameItem item in items)
		{
			foreach (Int2 area in item.areas)
			{
				UISprite uisprite = MakeSprite(itemLayer.transform, _itemSprites[item.type].sprite, area.row, area.column, 200);
				Tile t = tiles[area.row][area.column];
				t.itemType = item.type;
				t.itemSprite = uisprite;
			}
		}

		// Land Tile
		foreach (GameLand land in lands)
		{
			foreach (Int2 area in land.areas)
			{
				UISprite uisprite = MakeSprite(landLayer.transform, _landSprites[land.type].sprite, area.row, area.column, 300);
				Tile t = tiles[area.row][area.column];
				t.landType = land.type;
				t.landSprite = uisprite;
				NGUITools.AddWidgetCollider(t.landSprite.gameObject);
			}
		}
	}

	UISprite MakeSprite(Transform parent, string spriteName, int row, int column, int depth){
		UISprite uisprite = Instantiate(spritePrefab, parent);

		uisprite.atlas = atlas;
		uisprite.spriteName = spriteName;
		uisprite.pivot = UIWidget.Pivot.BottomLeft;
		uisprite.depth = depth - row;

		SetSpriteSize(uisprite);

		uisprite.transform.localPosition = new Vector3(column * tileSize - Screen.width / 2, row * tileSize - Screen.height / 2, 0);
		return uisprite;
	}

	void SetSpriteSize(UISprite uisprite)
	{
		float aspect = (float)uisprite.GetAtlasSprite().height / (float)uisprite.GetAtlasSprite().width;
		uisprite.aspectRatio = 1 / aspect;
		uisprite.width = tileSize;
		uisprite.height = (int) (tileSize * aspect);
	}

}
