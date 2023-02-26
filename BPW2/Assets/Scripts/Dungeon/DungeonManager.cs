using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DungeonManager : MonoBehaviour {
	public static DungeonManager instance;

	[SerializeField]
	private DungeonGenerator dgnGen;
	[SerializeField]
	private EnemyManager enemyManager;

	[SerializeField]
	private int maxSpawnRerol = 5;

	private float spawnOffset = 0.5f;

	private void Awake() {
		instance = this;
	}

	private void Start() {
		SpawnPlayer();
	}

	private void SpawnPlayer() {
		Vector3 position = dgnGen.allRooms[0].GetCenter();
		position.y += spawnOffset;

		Manager.instance.playerObj.position = position;
	}

	//spawn a random amount of enemies based on room size at a random location
	private void SpawnEnemies(int roomId) {
		int size = dgnGen.allRooms[roomId].roomSize;
		int amount = Mathf.CeilToInt(Random.Range(1, size * 0.01f));
		print("Spawn " + amount + " enemies.");

		Vector3[] positions = new Vector3[amount];
		int currentRerol = 0;

		for(int i = 0; i < amount; i++) {
			Vector3 newPos = dgnGen.allRooms[roomId].GetRandomPosition();
			newPos.y += 0.5f;

			if(!positions.Contains(newPos)) {
				positions[i] = newPos;
				enemyManager.SpawnEnemy(newPos);
			} else if(currentRerol < maxSpawnRerol) {
				i--;
				currentRerol++;
			}
		}
	}

	public void SpawnNewObject(Transform obj, int roomId) {
		Vector3 position = dgnGen.allRooms[roomId].GetRandomPosition();
		position.y += spawnOffset;

		Instantiate(obj, position, Quaternion.identity, transform);
	}

	public void CheckCurrentRoom(Vector3Int playerPos) {
		if(dgnGen.allTiles.ContainsKey(playerPos)) {
			int roomId = dgnGen.allTiles[playerPos].roomId;

			if(roomId >= 0 && !dgnGen.allRooms[roomId].visited) {
				SpawnEnemies(roomId);
				dgnGen.allRooms[roomId].visited = true;
				RaiseHallWalls();
			}
		}
	}

	public void RaiseHallWalls() {
		for(int i = 0; i < dgnGen.allHallWalls.Count; i++) {
			Vector3 pos = dgnGen.allHallWalls[i].position;
			pos.y = 0;

			dgnGen.allHallWalls[i].position = pos;
		}
	}

	public void LowerHallWalls() {
		for(int i = 0; i < dgnGen.allHallWalls.Count; i++) {
			Vector3 pos = dgnGen.allHallWalls[i].position;
			pos.y = -1;

			dgnGen.allHallWalls[i].position = pos;
		}
	}
}
