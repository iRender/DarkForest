using UnityEngine;
using System.Collections;

public class ObjectNamesManager{
	public enum ObjectType
	{
		None,
		Role,
		Grass,
		Box,
		Bullet,
		ViewPort
	}
	public const string roleCollGOName = "Anim";
	public const string grassCollGOName = "Grass";
	public const string boxCollGOName = "Chest";
	public const string bulletCollGOName = "Bullet";
	public const string vpCollGOName = "Bullet";

	public static ObjectType GetType(string go_name)
	{
		if (string.Equals (go_name, roleCollGOName)) {
			return ObjectType.Role;
		}
		else if (go_name.Contains(grassCollGOName))
		{
			return ObjectType.Grass;
		}
		else if (go_name.Contains(boxCollGOName))
		{
			return ObjectType.Box;
		}
		else if (go_name.Contains(bulletCollGOName))
		{
			return ObjectType.Bullet;
		}
		else if (go_name.Contains(vpCollGOName))
		{
			return ObjectType.ViewPort;
		}
		return ObjectType.None;
	}
}
