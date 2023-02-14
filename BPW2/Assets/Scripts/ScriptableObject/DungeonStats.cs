using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonStats", menuName = "ScriptableObjects/DungeonStats")]
public class DungeonStats : ScriptableObject {
	[Header("Grid stats")]
	public int mapWidth = 100;
	public int mapHeight = 100;
	public int TileSize = 1;
	public int minRoomSize = 3, maxRoomSize = 8;
	public int minRoomAmount = 2, maxRoomAmount = 10;

	[Header("Dungeon prefabs")]
	public GameObject groundPrefab;
	public GameObject wallPrefab;
	public GameObject hallWallPrefab;
}
