using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	[SyncVar] // 用来标记同步成员变量，可以是任何基本数据类型，但不能是类、列表或其他集合
	Vector3 synsPos;

	public float lerpRate = 15;

	void Update ()
	{
		if (!isLocalPlayer) {
			return;
		}

		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate (0, x, 0);
		transform.Translate (0, 0, z);
	}

	void FixedUpdate()
	{
		TransmitPosition();
		LerpPosition();
	}

	void LerpPosition()
	{
		if (!isLocalPlayer)
		{
			transform.position = Vector3.Lerp(transform.position, synsPos, Time.deltaTime);
		}
	}
	[Command] // 新的网络系统中的RPC,即在客户端调用，在服务器执行。还有一种是ClientRpcCalls,正好相反
	void CmdProvidePositionToServer(Vector3 pos) // 方法以Cmd开头，使用此命令的任意参数都会被传递到服务器端
	{
		synsPos = pos;
	}

	[ClientCallback] // 可作为成员函数的自定义属性
	void TransmitPosition()
	{
		if (isLocalPlayer)
		{
			CmdProvidePositionToServer(transform.position);
		}
	}
}
