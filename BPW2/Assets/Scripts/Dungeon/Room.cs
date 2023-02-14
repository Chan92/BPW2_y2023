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
}