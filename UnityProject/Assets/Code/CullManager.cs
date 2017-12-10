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

	private Dictionary<int, bool> _InGrassCache = new Dictionary<int, bool> ();
	private Dictionary<int, List<int>> _InVisionCache = new Dictionary<int, List<int>> ();

	private Dictionary<int, Camera> _CameraCache = new Dictionary<int, Camera> ();
	private Dictionary<int, int> _LayerCache = new Dictionary<int, int> ();

	void Start ()
	{
		Messenger.AddListener<int> (GameEventType.Role_InGrass, Role_InGrass);
		Messenger.AddListener<int> (GameEventType.Role_OutGrass, Role_OutGrass);

		Messenger.AddListener<int, int> (GameEventType.Role_Enter_OtherVision, Role_Enter_OtherVision);
		Messenger.AddListener<int, int> (GameEventType.Role_Exit_OtherVision, Role_Exit_OtherVision);
	}

	void Role_InGrass (int roleid)
	{
		_InGrassCache [roleid] = true;
			
		UpdateCameraVision ();
	}

	void Role_OutGrass (int roleid)
	{
		_InGrassCache [roleid] = false;

		UpdateCameraVision ();
	}

	void Role_Enter_OtherVision (int me, int other)
	{
		List<int> visionlist = _InVisionCache [me];
		if (visionlist == null) {
			visionlist = new List<int> ();
			_InVisionCache.Add (me, visionlist);
		}

		if (!visionlist.Contains (other)) {
			visionlist.Add (other);
		}

		UpdateCameraVision ();
	}

	void Role_Exit_OtherVision (int me, int other)
	{
		List<int> visionlist = _InVisionCache [me];
		if (visionlist == null) {
			return;
		}

		if (visionlist.Contains (other)) {
			visionlist.Remove (other);
		}

		UpdateCameraVision ();
	}

	void UpdateCameraVision ()
	{
		foreach (int id in _CameraCache.Keys) {
			Camera roleCamera = _CameraCache [id];

			foreach (int rid in _CameraCache.Keys) {
				if (rid == id) {
					Active_CameraCullPlayer (roleCamera, rid, true);
				} else {
					if (_InGrassCache [rid]) {
						List<int> visionlist = _InVisionCache [id];
						if (visionlist == null) {
							Active_CameraCullPlayer (roleCamera, rid, false);
							return;
						}
						Active_CameraCullPlayer (roleCamera, rid, visionlist.Contains (rid));
					} else {
						Active_CameraCullPlayer (roleCamera, rid, true);
					}
				}
			}
		}
	}

	public void Active_CameraCullPlayer (Camera c, int roleid, bool active)
	{
		int layer = _LayerCache [roleid];
		if (active) {
			c.cullingMask |= (1 << layer);
		} else {
			c.cullingMask &= ~(1 << layer);
		}
	}

	public void Register (CameraController cc, Role role)
	{
		int id = role.m_id;

		if (!_CameraCache.ContainsKey (id)) {
			_CameraCache.Add (id, cc.GetComponent<Camera> ());
			_InGrassCache.Add (id, false);
			_InVisionCache.Add (id, new List<int> ());
			_LayerCache.Add (id, role.gameObject.layer);
		}
	}

	void Update ()
	{
		
	}
}
