﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public const int maxHealth = 100;

	[SyncVar]
	public int currentHealth = maxHealth;

	public void TakeDamage(int amount)
	{
		if (!isServer)
		{
			return;
		}

		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Debug.Log("Dead!");
		}
	}
}
