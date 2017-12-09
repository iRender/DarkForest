using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	[SyncVar]  
	private Vector3 syncPlayerPos;

	[SyncVar]  
	private Quaternion syncPlayerRotate;

	private float speed = 10f;
	private float rotateSpeed = 60f;

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.red;
	}

	void Update ()
	{
		if (!isLocalPlayer) {
			return;
		}

		float Hor = Input.GetAxis ("Horizontal");
		float ver = Input.GetAxis ("Vertical");

		transform.Translate(Hor * Time.deltaTime * speed, 0, ver * speed * Time.deltaTime);  
		transform.Rotate(new Vector3(0, rotateSpeed * Hor * Time.deltaTime, 0)); 
	}

	void FixedUpdate()
	{
		//如果是本地创建 则把数据更新至服务器 通过SyncVar 发送给所有的客户端
		if (isLocalPlayer)
		{
			CmdSendServerPos(transform.position,transform.rotation);
		}
		else
		{
			//如果不是本地创建 则 插值移动（所谓的镜像移动）
			LerpPosition();
		}
	}

	//插值移动  
	void LerpPosition()
	{
		transform.position = Vector3.Lerp(transform.position, syncPlayerPos, 5 * Time.fixedDeltaTime);
		transform.rotation = Quaternion.Lerp(transform.rotation,syncPlayerRotate,5 * Time.fixedDeltaTime);
	}

	[Command] 
	public void CmdSendServerPos(Vector3 pos,Quaternion rotate)
	{
		syncPlayerPos = pos;
		syncPlayerRotate = rotate;
	}
}
