using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : LandTile 
{
	public UISpriteAnimation anim;
	public BoxCollider2D col;

	public string normal;
	public string burned;
	public string cutted;

	public string swayAnim;
	public string burnAnim;
	public string cutAnim;
	public string sprawnAnim;

	void Start()
	{
		anim.namePrefix = " ";
	}
	
	public void Sway()
	{
		anim.namePrefix = swayAnim;
	}

	public void Burn()
	{
		anim.namePrefix = burnAnim;
		sprite.spriteName = burned;
		col.enabled = false;
		sprite.spriteName = " ";
		ViewHide ();
	}

	public void ViewClear()
	{
		sprite.alpha = 0.3f;
	}

	public void ViewHide()
	{
		sprite.alpha = 1.0f;
	}

	public void Cut()
	{
		anim.namePrefix = cutAnim;
		sprite.spriteName = cutted;
	}

}
