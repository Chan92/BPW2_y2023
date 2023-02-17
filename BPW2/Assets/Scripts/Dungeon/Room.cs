using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
	public int minX, maxX, minZ, maxZ;

	public Room(int _minX, int _maxX, int _minZ, int _maxZ) {
		minX = _minX;
		maxX = _maxX;
		minZ = _minZ;
		maxZ = _maxZ;
	}

	public Vector3Int GetCenter() {
		int posX = Mathf.RoundToInt(Mathf.Lerp(minX, maxX, 0.5f));
		int posZ = Mathf.RoundToInt(Mathf.Lerp(minZ, maxZ, 0.5f));

		return new Vector3Int(posX, 0, posZ);
	}

	//Get any position exclude the tiles touching the wall
	//This helps to prevent objects from blocking the hallways
	public Vector3Int GetRandomPosition() {
		int posX = Mathf.RoundToInt(Random.Range(minX +1, maxX));
		int posZ = Mathf.RoundToInt(Random.Range(minZ +1, maxZ));

		return new Vector3Int(posX, 0, posZ);
	}
}