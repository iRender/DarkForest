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

	public IPlayerReciver playerReciver {
		get { 
			if (isLocalPlayer) {
				return transform.FindChild ("MyselfPlayer").GetComponent<IPlayerReciver> ();
			} else {
				return transform.FindChild ("Role").GetComponent<IPlayerReciver> ();
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




	[SyncVar (hook = "OnStateChanage")]
	public int mstate = 1;

	[ClientCallback]
	public void OnStateChanage (int state)
	{
		mstate = state;

		playerReciver.OnStateChanage (state);
	}

	[Command]
	public void Cmd_DoChangeState (int state)
	{
		mstate = state;
	}




	[Command]
	public void Cmd_DoAddBullet (GameObject bullet)
	{
		NetworkServer.Spawn (bullet);
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
