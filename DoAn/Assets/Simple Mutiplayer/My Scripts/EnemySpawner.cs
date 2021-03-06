﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {
	public GameObject enemyPrefab;
	public int numberOffEnemies;

	public override void OnStartServer()
	{
		for (int i = 0; i < numberOffEnemies; i++) {
			Vector3 spawnPosition = new Vector3 (Random.Range (-100.0f, 50.0f), 0.0f, Random.Range (-50.0f, 100.0f));
			Quaternion spawnRotation = Quaternion.Euler (0.0f, Random.Range (0.0f, 180.0f), 0);

			GameObject enemy = (GameObject)Instantiate (enemyPrefab, spawnPosition, spawnRotation);
			NetworkServer.Spawn (enemy);
		}
	}

}
