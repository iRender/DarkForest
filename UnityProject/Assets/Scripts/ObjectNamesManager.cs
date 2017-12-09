using UnityEngine;
using System.Collections;

public class ObjectNamesManager{
	public enum ObjectType
	{
		None,
		Role,
		Grass
	}
	public const string roleCollGOName = "Anim";
	public const string grassCollGOName = "Grass";

	public static ObjectType GetType(string go_name)
	{
		if (string.Equals (go_name, roleCollGOName)) {
			return ObjectType.Role;
		}
		else if (go_name.Contains(grassCollGOName))
		{
			return ObjectType.Grass;
		}
		return ObjectType.None;
	}
}
