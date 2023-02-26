using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tile {
	public TileType tileType;
	public Vector3Int tilePosition;
	public int roomId;

	//negative room number for tiles that arent part of a room (e.g.: halls)
	public Tile(Vector3Int _tilePosition, TileType _tileType) {
		tilePosition = _tilePosition;
		tileType = _tileType;
		roomId = -1;
	}

	public Tile(Vector3Int _tilePosition, TileType _tileType, int _roomId) {
		tilePosition = _tilePosition;
		tileType = _tileType;
		roomId = _roomId;
	}

	public void SpawnTile(GameObject tilePrefab, Transform tileParent) {
		GameObject.Instantiate(tilePrefab, tilePosition, Quaternion.identity, tileParent);
	}
}

public enum TileType {
	Ground,
	Wall
}