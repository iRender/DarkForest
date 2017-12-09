using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerData : NetworkBehaviour
{
	public int guid {
		get { 
			return (int)netId.Value;
		}	
	}

	public bool isMyself {
		get { 
			return isLocalPlayer;
		}
	}

	public Role role {
		get { 
			if (isLocalPlayer) {
				return transform.FindChild ("MyselfPlayer").GetComponent<Role> ();
			} else {
				return transform.FindChild ("Role").GetComponent<Role> ();
			}
		}
	}



	[SyncVar]
	public Vector3 synsPos;
	[SyncVar]
	public Quaternion synsQua;

	void FixedUpdate ()
	{
		//如果是本地创建 则把数据更新至服务器 通过SyncVar 发送给所有的客户端
		if (isLocalPlayer) {
			Cmd_SynsTransform ();
		} else {
			LerpTransform ();
		}
	}

	[Command]
	public void Cmd_SynsTransform ()
	{
		synsPos = transform.localPosition;
		synsQua = transform.localRotation;
	}

	void LerpTransform ()
	{
		transform.localPosition = Vector3.Lerp (transform.localPosition, synsPos, Time.deltaTime);
		transform.localRotation = Quaternion.Lerp (transform.localRotation, synsQua, 5 * Time.fixedDeltaTime);
	}


	//	[SyncVar (hook = "OnHpChange")]
	//	public int hp = 100;
	//
	//	public void OnHpChange (int shp)
	//	{
	//		Debug.Log (shp);
	//		Debug.Log (hp);
	//		Debug.Log ("Hp Change");
	//	}
	//

	[SyncVar (hook = "OnStateChanage")]
	public int mstate = 1;

	[ClientCallback]
	public void OnStateChanage (int state)
	{
		role.OnStateChanage (state);
	}

	[Command]
	public void Cmd_DoChangeState (int state)
	{
		mstate = state;
	}


	public GameObject MyselfPlayer;
	public GameObject Role;

	void Start ()
	{
		MyselfPlayer.gameObject.SetActive (isLocalPlayer);
		Role.gameObject.SetActive (!isLocalPlayer);

		GameObject.DontDestroyOnLoad (this.gameObject);
	}
}
