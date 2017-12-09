using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	[SyncVar]  
	public Vector3 syncPlayerPos;

	[SyncVar]  
	public Quaternion syncPlayerRotate;

	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	public float speed = 10f;
	public float rotateSpeed = 60f;

	public override void OnStartLocalPlayer ()
	{
		GetComponent<MeshRenderer> ().material.color = Color.red;
	}

	void Update ()
	{
		if (!isLocalPlayer) {
			return;
		}

		float Hor = Input.GetAxis ("Horizontal");
		float ver = Input.GetAxis ("Vertical");

		transform.Translate (Hor * Time.deltaTime * speed, 0, ver * speed * Time.deltaTime);  
		transform.Rotate (new Vector3 (0, rotateSpeed * Hor * Time.deltaTime, 0)); 

		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdFire ();
		}
	}

	void FixedUpdate ()
	{
		//如果是本地创建 则把数据更新至服务器 通过SyncVar 发送给所有的客户端
		if (isLocalPlayer) {
			CmdSendServerPos (transform.position, transform.rotation);
		} else {
			//如果不是本地创建 则 插值移动（所谓的镜像移动）
			LerpPosition ();
		}
	}

	//插值移动
	void LerpPosition ()
	{
		transform.position = Vector3.Lerp (transform.position, syncPlayerPos, 5 * Time.fixedDeltaTime);
		transform.rotation = Quaternion.Lerp (transform.rotation, syncPlayerRotate, 5 * Time.fixedDeltaTime);
	}

	[Command] 
	public void CmdSendServerPos (Vector3 pos, Quaternion rotate)
	{
		syncPlayerPos = pos;
		syncPlayerRotate = rotate;
	}

	[Command]
	void CmdFire ()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (
			             bulletPrefab,
			             bulletSpawn.position,
			             bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 6;

		// Spawn the bullet on the Clients
		NetworkServer.Spawn (bullet);

		// Destroy the bullet after 2 seconds
		Destroy (bullet, 2.0f);        
	}
}
