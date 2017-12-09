using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerData : NetworkBehaviour
{
	public static PlayerData Create (GameObject player)
	{
		PlayerData pd = player.GetComponent<PlayerData> ();
		if (pd == null) {
			pd = player.AddComponent <PlayerData> ();	
		}
		return pd;
	}

	[SyncVar (hook = "OnHpChange")]
	public int hp = 100;

	public void OnHpChange (int shp)
	{
		Debug.Log (shp);
		Debug.Log (hp);
		Debug.Log ("Hp Change");
	}

	[SyncVar]
	public int skill = 1;

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
		synsPos = transform.position;
		synsQua = transform.rotation;
	}

	void LerpTransform ()
	{
		transform.position = Vector3.Lerp (transform.position, synsPos, Time.deltaTime);
		transform.rotation = Quaternion.Lerp (transform.rotation, synsQua, 5 * Time.fixedDeltaTime);
	}
}
