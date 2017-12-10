using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CullManager :MonoBehaviour
{
	static CullManager _me;

	public static CullManager Me {
		get { 
			if (_me != null) {
				return _me;
			}

			GameObject go = new GameObject ("CullManager");
			_me = go.AddComponent<CullManager> ();

			return _me;
		}
	}

	private Dictionary<int, Camera> _CameraCache = new Dictionary<int, Camera> ();

	void Start ()
	{
		Messenger.AddListener<int> (GameEventType.Role_InGrass, Role_InGrass);
		Messenger.AddListener<int> (GameEventType.Role_OutGrass, Role_OutGrass);
	}

	void Role_InGrass (int roleid)
	{
		Camera c = _CameraCache [roleid];
		if (c != null) {
			foreach (var v in _CameraCache) {
				Active_CameraCullPlayer (c, roleid, v.Key == roleid);
			}
		}
	}

	void Role_OutGrass (int roleid)
	{
		Camera c = _CameraCache [roleid];
		if (c != null) {
			foreach (var v in _CameraCache) {
				Active_CameraCullPlayer (c, roleid, true);
			}
		}
	}

	public void Active_CameraCullPlayer (Camera c, int roleid, bool active)
	{
		int layer = LayerMask.NameToLayer ("player" + roleid);
		if (active) {
			c.cullingMask |= (1 << layer);
		} else {
			c.cullingMask &= ~(1 << layer);
		}
	}

	public void Register (CameraController cc, Role role)
	{
		_CameraCache.Add (role.m_id, cc.GetComponent<Camera> ());
	}

	void Update ()
	{
		
	}
}
