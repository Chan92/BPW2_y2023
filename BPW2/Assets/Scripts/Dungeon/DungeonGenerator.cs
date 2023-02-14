using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DungeonGenerator : MonoBehaviour {
	public List<Room> allRooms = new List<Room>();
	public List<Transform> allHallWalls = new List<Transform>();
	public Dictionary<Vector3Int, Tile> allTiles = new Dictionary<Vector3Int, Tile>();

	[SerializeField]
	private DungeonStats dungeonStats;
	private int maxLoopFail = 5;

	private void Awake() {
		GenerateDungeon();
	}

	public void GenerateDungeon() {
		GenerateRooms();
		GenerateHalls();
		GenerateWalls();
	}

	private void GenerateRooms() {
		int roomAmount = Random.Range(dungeonStats.minRoomAmount, dungeonStats.maxRoomAmount);
		int loopFailCheck = 0;

		for(int i = 0; i < roomAmount; i++) {
			int minX = Random.Range(0, dungeonStats.mapWidth - dungeonStats.maxRoomSize);
			int maxX = minX + Random.Range(dungeonStats.minRoomSize, dungeonStats.maxRoomSize + 1);
			int minZ = Random.Range(0, dungeonStats.mapHeight - dungeonStats.maxRoomSize);
			int maxZ = minZ + Random.Range(dungeonStats.minRoomSize, dungeonStats.maxRoomSize + 1);

			Room newRoom = new Room(minX, maxX, minZ, maxZ);
			if(CanRoomFitInDungeon(newRoom)) {
				AddRoomToDungeon(newRoom, i);
				loopFailCheck = 0;
			} else {
				//cancel generating a room if regenerate isnt possible after a few tries in a row
				if(loopFailCheck < maxLoopFail) {
					i--;
					loopFailCheck++;
				}
			}
		}

		Manager.instance.StartRoomCenter = allRooms[0].GetCenter();
	}

	private void GenerateHalls() {
		for(int i = 0; i < allRooms.Count - 1; i++) {
			Vector3Int positionA = allRooms[i].GetCenter();
			Vector3Int positionB = allRooms[i + 1].GetCenter();

			int dirX = positionB.x > positionA.x ? 1 : -1;
			int x = 0;
			for(x = positionA.x; x != positionB.x; x += dirX) {
				Vector3Int pos = new Vector3Int(x, 0, positionA.z);
				if(allTiles.ContainsKey(pos)) {
					continue;
				}

				AddTile(pos, TileType.Ground);
				AddHallWalls(pos);
			}

			int dirZ = positionB.z > positionA.z ? 1 : -1;
			for(int z = positionA.z; z != positionB.z; z += dirZ) {
				Vector3Int pos = new Vector3Int(x, 0, z);
				if(allTiles.ContainsKey(pos)) {
					continue;
				}

				AddTile(pos, TileType.Ground);
				AddHallWalls(pos);
			}
		}
	}

	private void GenerateWalls() {
		var keys = allTiles.Keys.ToList();
		foreach(var kv in keys) {
			for(int x = -1; x <= 1; x++) {
				for(int z = -1; z <= 1; z++) {
					Vector3Int newPos = kv + new Vector3Int(x, 0, z);
					if(allTiles.ContainsKey(newPos)) {
						continue;
					}
					AddTile(newPos, TileType.Wall);
				}
			}
		}
	}

	private void AddTile(Vector3Int position, TileType type) {
		Tile newTile = new Tile(position, type);
		allTiles.Add(position, newTile);

		switch(type) {
			case TileType.Ground:
				newTile.SpawnTile(dungeonStats.groundPrefab, transform);
				break;
			case TileType.Wall:
				newTile.SpawnTile(dungeonStats.wallPrefab, transform);
				break;
		}
	}

	//blocking the halls from entering before clearing the room
	private void AddHallWalls(Vector3Int position) {
		GameObject newHallWall = Instantiate(dungeonStats.hallWallPrefab, position, Quaternion.identity, transform);
		allHallWalls.Add(newHallWall.transform);
	}

	private void AddTile(Vector3Int position, TileType type, int roomId) {
		Tile newTile = new Tile(position, type, roomId);
		allTiles.Add(position, newTile);

		switch(type) {
			case TileType.Ground:
				newTile.SpawnTile(dungeonStats.groundPrefab, transform);
				break;
			case TileType.Wall:
				newTile.SpawnTile(dungeonStats.wallPrefab, transform);
				break;
		}
	}

	private void AddRoomToDungeon(Room room, int roomId) {
		for(int x = room.minX; x < room.maxX; x++) {
			for(int z = room.minZ; z < room.maxZ; z++) {
				Vector3Int position = new Vector3Int(x, 0, z);
				AddTile(position, TileType.Ground, roomId);
			}
		}

		allRooms.Add(room);
	}

	private bool CanRoomFitInDungeon(Room room) {
		for(int x = room.minX - 1; x < room.maxX + 1; x++) {
			for(int z = room.minZ - 1; z < room.maxZ + 1; z++) {
				if(allTiles.ContainsKey(new Vector3Int(x, 0, z))) {
					return false;
				}
			}
		}

		return true;
	}
}
