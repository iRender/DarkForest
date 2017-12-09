using UnityEngine;
using System.Collections;

public class Button_Match : MonoBehaviour
{
	public GameNetDiscover gnd;

	void OnClick ()
	{
		gnd.Run ();
	}
}
