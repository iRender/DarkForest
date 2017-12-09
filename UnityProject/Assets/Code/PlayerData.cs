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
		synsPos = transform.localPosition;
		synsQua = transform.localRotation;
	}

	void LerpTransform ()
	{
		transform.localPosition = Vector3.Lerp (transform.localPosition, synsPos, Time.deltaTime);
		transform.localRotation = Quaternion.Lerp (transform.localRotation, synsQua, 5 * Time.fixedDeltaTime);
	}


	void OnCollisionEnter (Collision collision)
	{
		Debug.Log ("OnCollisionEnter In");
		if (collision.gameObject.name == "Bullet") {
			Debug.Log ("OnCollisionEnter In");
			Debug.Log (hp);

			hp = hp - 1;
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		Debug.Log (coll.gameObject.name);
	}

	public GameObject MyselfPlayer;
	public GameObject Role;

	void Start ()
	{
		MyselfPlayer.gameObject.SetActive (isLocalPlayer);
		Role.gameObject.SetActive (!isLocalPlayer);
	}
}
