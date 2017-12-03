using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Heatlh : NetworkBehaviour {

	public const int maxHeatlh = 100;
	[SyncVar (hook="OnChangeHealth")]public int currentHeatlh = maxHeatlh;
	public RectTransform healthBar;
	public bool destroyOnDeath;
	private NetworkStartPosition[] spawnPoints;
		
	void Start()
	{
		if (isLocalPlayer) {
			spawnPoints = FindObjectsOfType<NetworkStartPosition> ();
		}
	}

	public void TakeDame(int amount) 
	{
		if (!isServer) {
			return;
		}
		currentHeatlh -= amount;
		if (currentHeatlh <= 0) {
			if (destroyOnDeath) {
				Destroy (gameObject);
			} else {
				currentHeatlh = maxHeatlh;
				RpcRespawn ();
			}

		}
		healthBar.sizeDelta = new Vector2 (currentHeatlh * 2, healthBar.sizeDelta.y);
	}
	void OnChangeHealth(int health)
	{
		healthBar.sizeDelta = new Vector2 (health * 2, healthBar.sizeDelta.y);
	}
	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer) {
			Vector3 spawnPoint = Vector3.zero;

			if (spawnPoints != null && spawnPoints.Length > 0) {
				spawnPoint = spawnPoints [Random.Range (0, spawnPoints.Length)].transform.position;
			}
			transform.position = spawnPoint;
		}
	}
}
