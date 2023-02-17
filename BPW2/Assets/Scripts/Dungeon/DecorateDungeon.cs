using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorateDungeon : MonoBehaviour {
	[SerializeField]
	private DungeonGenerator dgnGen;
	[SerializeField]
	private Transform playerObj;
	[SerializeField]
	private Transform[] enemyObjs;

	private void Start() {
		SpawnPlayer();

		SpawnEnemies(1, 0);
	}

	private void SpawnPlayer() {
		Vector3 position = dgnGen.allRooms[0].GetCenter();
		position.y += 0.5f;

		playerObj.position = position;
	}

	public void SpawnEnemies(int amount, int roomId) {
		if(amount > enemyObjs.Length) {
			amount = enemyObjs.Length;
		}

		for(int i = 0; i < amount; i++) {
			Vector3 position = dgnGen.allRooms[roomId].GetRandomPosition();
			position.y += 0.5f;

			enemyObjs[i].position = position;
		}
	}

	public void SpawnNewObject(Transform obj, int roomId) {
		Vector3 position = dgnGen.allRooms[roomId].GetRandomPosition();
		position.y += 0.5f;

		Instantiate(obj, position, Quaternion.identity, transform);
	}
}
